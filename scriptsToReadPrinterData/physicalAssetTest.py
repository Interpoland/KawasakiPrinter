import VTDataRepository
import time

#changes made to loaded items will not be saved to the repository!
def main():
	#initialize repository
	repo = VTDataRepository.VTDataRepository("http://10.42.0.18:8080/")

	#load a class from the repository
	class1 = repo.loadClass("testClass1")
	print("class1 UUID: " + class1.UUID)
	print("class1 description: " + class1.description)

	#load a property from the repository
	property1 = repo.loadProperty("testProperty1")
	print("property1 UUID: " + property1.UUID)
	print("property1 description: " + property1.description)
	
	#create a new class
	class2 = repo.createClass("testClass9", "this class was created using the python api")
	
	#create a new property
	property2= repo.createProperty("testProperty9", "this property was created using the python api", "value")

	#create a new physical asset
	physicalAsset1 = repo.createPhysicalAsset("PhysicalAsset01", classes=[class1,class2], properties=[property1, property2])	
	print(physicalAsset1.toString())

	#set connections string for an asset
	res = physicalAsset1.setConnectionString("usb-FTDI_FT232R_USB_UART_A7049C0Z-if00-port0")
	print(res)

	#add dataTag xPosit (integer) to asset1
	tag = physicalAsset1.createDataTag("xPosit", VTDataRepository.RepositoryDataType.INTEGER)

	#push data point to tag (auto generate time)
	tag.pushPoint(23, quality=192)

	#push data point to tag every second (auto generate time and quality)
	for x in range(100):
		tag.pushPoint(x)
		time.sleep(.5)

	#load a physical asset
	PA2 = repo.loadPhysicalAsset("PhysicalAsset01")
	print(PA2.toString())

	#load asset from primary connection string
	asset = repo.getAssetFromConnectionString("testAuth")
	print(asset)

	#load asset from primary and secondary connection string
	asset2 = repo.getAssetFromConnectionString("someAuthString1", "someAuthString2")
	print(asset2)
	
	#update and existing physical asset
	class3 = repo.createClass("updateTest", "testing update via python client")
	PA2.addClass(class3, True)

main()
