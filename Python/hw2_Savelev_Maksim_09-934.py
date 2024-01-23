import numpy as np
import matplotlib.pyplot as plt
from math import sqrt


def point_distance(first, second):
    return sqrt((first[0]-second[0])**2+(first[1]-second[1])**2)


def draw_line(first, second, plot, koeff):
    x = first[0]
    y = first[1]

    x_step = (second[0] - first[0])/50
    y_step = (second[1] - first[1])/50

    for i in range(50):
        plot[int(y), int(x)] = [255/255*(1 - point_distance(first, second)*koeff/max(plot.shape)), 0, 0]
        x += x_step
        y += y_step

vertex = []
facets = []
with open(r'C:\\Users\\Nouri\\Desktop\\gruph\\point.obj', 'r', encoding='utf-8') as f:
    for i in f.readlines():
        elem = i.split(' ')

        if elem[0] == 'v':
            vertex.append([float(elem[1]), float(elem[2]), float(elem[3][:-1])])
        elif elem[0] == 'f':
            facets.append([int(elem[1]), int(elem[2]), int(elem[3][:-1])])

x = []
y = []
for i in range(len(facets)):
    x.append(vertex[facets[i][0]-1][0])
    x.append(vertex[facets[i][1]-1][0])
    x.append(vertex[facets[i][2]-1][0])
    y.append(vertex[facets[i][0]-1][1])
    y.append(vertex[facets[i][1]-1][1])
    y.append(vertex[facets[i][2]-1][1])
    facets[i] = [vertex[facets[i][0]-1][0:2], vertex[facets[i][1]-1][0:2], vertex[facets[i][2]-1][0:2]]

koeff = 30

min_x = min(x)
min_y = min(y)
x_len = max(x)*koeff
y_len = max(y)*koeff
middle_x = x_len/2
middle_y = y_len/2

for i in range(len(facets)):
    facets[i][0][0] += abs(min_x)
    facets[i][0][0] *= koeff*0.45
    facets[i][1][0] += abs(min_x)
    facets[i][1][0] *= koeff*0.45
    facets[i][2][0] += abs(min_x)
    facets[i][2][0] *= koeff*0.45

    facets[i][0][1] += abs(min_y)
    facets[i][0][1] *= koeff*0.45
    facets[i][1][1] += abs(min_y)
    facets[i][1][1] *= koeff*0.45
    facets[i][2][1] += abs(min_y)
    facets[i][2][1] *= koeff*0.45

plot = np.ones(shape=(int(x_len), int(y_len), 3), dtype=float)

for i in facets:
    draw_line(i[0], i[1], plot, koeff)
    draw_line(i[1], i[2], plot, koeff)
    draw_line(i[2], i[0], plot, koeff)

plt.figure(figsize=(12, 12))
plt.imshow(plot, origin='lower')
plt.axis('off')
plt.savefig('C:\\Users\\Nouri\\Desktop\\gruph\\image.png')
plt.show()