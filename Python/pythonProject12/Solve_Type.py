import math
def solve(a, b, c):
    if a == 0:
        # bx + c = 0
        # 1. b==0\
            # a. c == 0
                # 0x^2 + 0x+0=0
            # b. c != 0
                # no solution
        # 2. b!=0

        x = -c/b
        return x
    else:
        Discr = pow(b,2)-4*a*c
        if Discr < 0:
            return "Решений нет"
        elif Discr == 0:
            return -b/(2*a)
        else:
            x1 = (-b+math.sqrt(Discr))/(2*a)
            x2 = (-b-math.sqrt(Discr))/(2*a)
            return x1, x2


