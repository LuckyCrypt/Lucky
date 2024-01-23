import string
def Str_Space():
    with open('Text.txt', 'r',encoding="utf-8") as f:
        rows = f.readline()
    NewStr = " ".join(rows.split())
    return NewStr
def ABS_count():
    chars = {}
    with open('Text1.txt', 'r',encoding="utf-8") as f:
        rows = f.readline()
    for char in rows:
        if char.isalpha():
            if char.lower() in chars:
                chars[char.lower()] += 1
            else:
                chars[char.lower()] = 1
    return chars

def brackets(line):
    cou = 0
    for i in line:
        cou += 1 if i == '(' else -1 if i == ')' else 0
        if cou < 0:
            return False
    return cou == 0

def Palindrom(StrName):
    lower = str(StrName).replace(" ", "").lower()
    if lower == lower[::-1]:
        return "Palindrome"
    else:
        return "Not Palindrome"
