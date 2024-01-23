import numpy as np
from math import cos, sin
from matplotlib import pyplot as plt
from matplotlib.animation import FuncAnimation
from random import random

figure = plt.figure(figsize=(5, 5))
plot = figure.add_subplot(1, 1, 1)

move = [random()*20, random()*20]
pos = [125, 375]
angle_move = 0.5
angle = 0

def draw_circle(plot, color):
    for i in range(-100, 100):
        for j in range(-100, 100):
            if abs(i*i) + abs(j*j) < 100*100:
                plot[i+250][j+250] = color

def draw_square(plot, color):
   for i in range(500):
        for j in range(500):
            if i > 450 or i < 50 or j > 450 or j < 50:
                plot[i][j] = color

def draw_line(x1, y1, x2, y2, plot, color):
    global triange_color

    x = x1
    y = y1

    x_step = (x2 - x1)/50
    y_step = (y2 - y1)/50

    collision = False
    x_y = [-1, -1]      

    for i in range(50):
        if (plot[int(y), int(x)][0] == 1 and plot[int(y), int(x)][1] == 1 and plot[int(y), int(x)][2] == 1) or (plot[int(y), int(x)][0] == triange_color[0] and plot[int(y), int(x)][1] == triange_color[1] and plot[int(y), int(x)][2] == triange_color[2]):
            plot[int(y), int(x)] = color
        else:
            collision = True
            x_y = [int(y), int(x)]

        x += x_step
        y += y_step

    return collision, x_y

def rotate(x, y):
    global pos, angle

    xr = cos(angle) * (x - pos[0]) - sin(angle) * (y - pos[1]) + pos[0]
    yr = sin(angle) * (x - pos[0]) + cos(angle) * (y - pos[1]) + pos[1]
    return xr, yr

def draw_triange(plot, color):
    global angle, pos

    x1, y1 = rotate(pos[0]-15, pos[1]-25)
    x2, y2 = rotate(pos[0]+25, pos[1])
    x3, y3 = rotate(pos[0]-15, pos[1]+25)

    collision_1, x_y_1 = draw_line(x1, y1, x2, y2, plot, color)
    collision_2, x_y_2 = draw_line(x2, y2, x3, y3, plot, color)
    collision_3, x_y_3 = draw_line(x3, y3, x1, y1, plot, color)

    if x_y_1[0] != -1:
        return True, x_y_1
    elif x_y_2[0] != -1:
        return True, x_y_2
    elif x_y_3[0] != -1:
        return True, x_y_3

    return False, x_y_1

circle_color = (0.5, 0, 0)
square_color = (0, 0.5, 0)
triange_color = (0, 0, 0.5)

def animate(_):
    figure.clear()

    plot = np.ones(shape=(500, 500, 3), dtype=float)

    global move, circle_color, square_color, triange_color, angle_move, angle

    angle += angle_move

    if abs(angle) > 6.28:
        angle = 0
        angle_move *= -1

    pos[0] += move[0]
    pos[1] += move[1]

    draw_circle(plot, circle_color)
    draw_square(plot, square_color)
    collision, x_y = draw_triange(plot, triange_color)

    if collision:
        if x_y[0] > 450 or x_y[0] < 50 or x_y[1] > 450 or x_y[1] < 50:
            square_color, triange_color = triange_color, square_color
        else:
            circle_color, triange_color = triange_color, circle_color

        move = [20*random(), 20*random()]

        if random() > 0.5:
            move[0] *= -1
        if random() > 0.5:
            move[1] *= -1

    plt.imshow(plot, origin='lower')

animation = FuncAnimation(figure, animate, interval=100, save_count=20)
plt.show()
animation.save('video.gif'  )
