tempData = "ok N0 P15 B15 T:21.0 /0.0 B:21.8 /0.0 T0:21.0 /0.0 @:0 B@:0"
posData = "X:0.00 Y:0.00 Z:0.00 E:0.00 Count X: 0.00 Y:0.00 Z:0.00"
brokeData = "ok N0 P15 B15"
hyrelData = "b'>RT :T13 999 0 4094 4094 :T91 23 0 23 58\n"

sizeTempData = len(tempData.split())
sizePosData = len(posData.split())
sizeHyrelData = len(hyrelData.split())

print(str(sizeTempData) + " is the length of tempData.")
print(str(sizePosData) + " is the length of posData.")
print(str(sizeHyrelData) + " is the length of the hyrelData.")
extruderTempData = hyrelData.split()[2].strip(":")
bedTempData = hyrelData.split()[7].strip(":")
print(bedTempData)
print(extruderTempData)
print(len(hyrelData))



print(tempData.split()[7])



