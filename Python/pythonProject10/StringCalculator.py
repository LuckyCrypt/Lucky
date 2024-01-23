import re


def EmptyString(numbers):
    sumNumbers = 0
    if len(numbers) > 2:
        if numbers[1] == "/" and numbers[3] == "\n":
            splited_num = numbers.split('\n',1)
            splited_num = splited_num[1].split(numbers[2])
            for i in splited_num:
                if i.isdigit() == True:
                    sumNumbers += int(i)
            return sumNumbers
        elif ',' in numbers:
            splited_num = numbers.split(',')
            for i in splited_num:
                if i.isdigit() == True:
                    sumNumbers += int(i)
            return sumNumbers
    elif numbers:
        return int(numbers)
    return 0
