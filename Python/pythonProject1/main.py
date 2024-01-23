import numpy as np

import matplotlib.pyplot as plt

from matplotlib.image import imsave


def create_image(heigh, widht, background_color):
    img = np.zeros((heigh, widht, 4), np.uint8)

    img[:, :, :3] = background_color

    img[:, :, 3] = 255

    return img


def set_color(img, x, y, color):
    img[x, y, :3] = color

    return img


def draw_line(img, x0, y0, x1, y1, color):
    # Bresenham algorithm

    steps_num = int(np.max([np.abs(x0 - x1), np.abs(y0 - y1)]))

    sp = np.linspace(0, 1, steps_num + 1)

    x_coords = np.int32(np.round(x0 * sp + x1 * (1 - sp)))

    y_coords = np.int32(np.round(y0 * sp + y1 * (1 - sp)))

    x_ind = (x_coords > 0) & (x_coords < img.shape[0])

    y_ind = (y_coords > 0) & (y_coords < img.shape[0])

    ind = x_ind & y_ind

    x_coords = x_coords[ind]

    y_coords = y_coords[ind]

    img = set_color(img, x_coords, y_coords, color)

    return img


def show_image(img):
    img = np.flipud(img)

    plt.figure()

    plt.imshow(img)

    plt.show()

    return 0


def save_image(img, name):
    img = np.flipud(img)

    imsave(name, img)

    return 0


def create_hermite_spline():
    h = 1024
    w = 1024
    black = np.array([0, 0, 0], np.uint8)
    green = np.array([0, 255, 0], np.uint8)
    red = np.array([255, 0, 0], np.uint8)
    img = create_image(h, w, black)
    N = 10  # points number
    x = np.random.randint(50, w - 50, 10)
    x = np.sort(x)
    y = np.random.randint(50, h - 50, 10)
    d = 20 * np.random.rand(10) - 10

    for i in range(N - 1):
        m = np.array([[1, x[i], x[i] ** 2, x[i] ** 3], [1, x[i + 1], x[i + 1] ** 2, x[i + 1] ** 3],
                      [0, 1, 2 * x[i], 3 * x[i] ** 2], [0, 1, 2 * x[i + 1], 3 * x[i + 1] ** 2]])
        b = np.array([y[i], y[i + 1], d[i], d[i + 1]]).T
        a = np.linalg.inv(m).dot(b)

        for j in range(x[i], x[i + 1] + 1):
            y_j = a[0] + a[1] * j + a[2] * j ** 2 + a[3] * j ** 3

            y_j_1 = a[0] + a[1] * (j + 1) + a[2] * (j + 1) ** 2 + a[3] * (j + 1) ** 3

            draw_line(img, j, y_j, j + 1, y_j_1, green)

    set_color(img, x, y, red)
    set_color(img, x + 1, y, red)
    set_color(img, x - 1, y, red)
    set_color(img, x, y + 1, red)
    set_color(img, x, y - 1, red)

    x1 = np.random.randint(50, w - 50, 10)
    x1 = np.sort(x1)
    y1 = np.random.randint(50, h - 50, 10)
    d1 = 20 * np.random.rand(10) - 10

    for i in range(N - 1):
        m1 = np.array([[1, x1[i], x1[i] ** 2, x1[i] ** 3], [1, x1[i + 1], x1[i + 1] ** 2, x1[i + 1] ** 3],
                      [0, 1, 2 * x1[i], 3 * x1[i] ** 2], [0, 1, 2 * x1[i + 1], 3 * x1[i + 1] ** 2]])
        b1 = np.array([y1[i], y1[i + 1], d1[i], d1[i + 1]]).T
        a = np.linalg.inv(m).dot(b)
        for j in range(x1[i], x1[i + 1] + 1):
            y_j = a[0] + a[1] * j + a[2] * j ** 2 + a[3] * j ** 3

            y_j_1 = a[0] + a[1] * (j + 1) + a[2] * (j + 1) ** 2 + a[3] * (j + 1) ** 3

            draw_line(img, j, y_j, j + 1, y_j_1, red)



    img = np.transpose(img, axes=(1, 0, 2))

    # save_image(img, 'hermite_spline.tga')

    show_image(img)


if __name__ == '__main__':
    create_hermite_spline()