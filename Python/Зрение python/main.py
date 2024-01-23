from PIL import Image
import PIL.ImageOps
from pylab import *
from matplotlib.pyplot import imshow

im = Image.open('empire.jpg')

def myshow(img, title=None):
    plt.figure(figsize=(6, 6))
    plt.imshow(img)
    if title is not None:
        plt.title(title)

    plt.axis('off')
    show()


def negativ(img):# задание 2
    return 255 - img

def schitat(im):#задание 1
    return np.array(im)

def ysredn(img):
    return compute_average(img)

def poluton(img):
    im = array(Image.open(img).convert('L'))
    Image.register_save(im)

#myshow(negativ(schitat(im)), 'negat')
#myshow(ysredn(im),'ysred')
poluton(im)