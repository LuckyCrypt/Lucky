import numpy
# import tkinter as tk
#
# window = tk.Tk()
#
# button = tk.Button(
#     text="Нажми на меня!",
#     width=25,
#     height=5,
#     bg="blue",
#     fg="yellow",
# )
#
# button.pack()
# window.geometry("1600x800")
# window.mainloop()
import math

import numpy as np


def codeVerticale(n,M,K,message):
    digit = math.ceil(n/4)#Количество блоков
    slog = n % 4 #Количество недостающих символов
    if slog == 0:
        massivDecode = []
        massivCode = np.empty(len(message), str)
        massivDecode += message
        for i in range(digit):
            massivCode[1+i*4] = massivDecode[0+(i*4)]
            massivCode[3+i*4] = massivDecode[1+(i*4)]
            massivCode[0+i*4] = massivDecode[2+(i*4)]
            massivCode[2+i*4] = massivDecode[3+(i*4)]

        for j in range(4):
            for i in range(digit):
                print(massivCode[j+i*4])
    else:
        massivDecode = np.empty((M, K), str)
        massivCode = np.empty((M, K), str)
        for o in message:
            i = 0
            j = 0
            massivCode[i][j] = message
            j += 1
            if j == 3:
                j = 0
                i = 1
        for i in range(slog):
            massivDecode += '\0'

        print(massivDecode)
        for i in range(digit):
            massivCode[1 + i * 4] = massivDecode[0 + (i * 4)]
            massivCode[3 + i * 4] = massivDecode[1 + (i * 4)]
            massivCode[0 + i * 4] = massivDecode[2 + (i * 4)]
            massivCode[2 + i * 4] = massivDecode[3 + (i * 4)]
        print(massivCode)
        for j in range(4):
            for i in range(digit):
                print(massivCode[j + i * 4], end='')

def decodeVerticale(message):
    massivDecode = []
    massivCode = np.empty(len(message), str)
    massivDecode += message
    digit = math.ceil(len(massivCode) / 4)

    for i in range(digit):
        massivCode[0 + (i * 4)] = massivDecode[1 + (i * 4)]
        massivCode[1 + (i * 4)] = massivDecode[3 + (i * 4)]
        massivCode[2 + (i * 4)] = massivDecode[0 + (i * 4)]
        massivCode[3 + (i * 4)] = massivDecode[2 + (i * 4)]

#0|4|1|5|2|6|3|7
codeVerticale(10,2,4,'abcdefghty')
#decodeVerticale("cgaedhbf")