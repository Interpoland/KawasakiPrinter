#convert connection to (connection, asset) tupple.

from enum import Enum
import requests
import datetime

def isList(item):
	if(not isinstance(item, list)):
		return False
	else:
		return True

def doesExist(item):
	if(item is None):
		return False
	else:
		return True

class VTDataRepository():
	#initialize URL
	def __init__(self, url="http://10.42.0.2:8080/"):
		try:
			self.url = self.validateURL(url)
			if self.connect():
				print("initialized at " + self.url)
			else:
				raise Exception("connection failed")
		except:
			raise Exception("could not initialize Repository object")

	#only removes trailing '/'
	def validateURL(self, url):
		if url.endswith("/"):
			url = url[0:len(url)-1]
		return url

	#attempts to connect to the /api/status endpoint
	def connect(self):
		resp = requests.get(self.url + "/api/status")
		if(resp.json()["status"] == "good"):
			return True
		else:
			return False

	def createPhysicalAsset(self, UUID, classes=None, properties=None, tags=None):
		result = RepositoryPhysicalAsset(self.url, UUID)

		if(doesExist(classes)):
			result.addClass(classes)

		if(doesExist(properties)):
			result.addProperty(properties)

		if(doesExist(tags)):
			result.addTag(tags)

		self._writePhysicalAsset(result)
		return result

	def getAssetFromConnectionString(self, primaryAuth, secondaryAuth=None):
		if secondaryAuth is None:
			response = requests.get(self.url+"/api/physicalAsset/auth/"+primaryAuth)
		else:
			response = requests.get(self.url+"/api/physicalAsset/auth/"+primaryAuth+"/"+secondaryAuth)
		return response.text

	def createClass(self, UUID, description):
		result = RepositoryClass(self.url, UUID, description)
		self._writeClass(result)
		return result

	def createProperty(self, UUID, description, value):
		result = RepositoryProperty(self.url, UUID, description, value)
		self._writeProperty(result)
		return result

	def loadClass(self, UUID):
		response = requests.get(self.url+"/api/class/"+UUID)
		json = response.json()
		if(json["status"] == "good"):
			return RepositoryClass(self.url, json["class"]["UUID"], json["class"]["description"])
		else:
			raise Exception("Error finding class")

	def loadPhysicalAsset(self, UUID):
		response = requests.get(self.url+"/api/physicalAsset/"+UUID)
		json = response.json()

		if json["status"] == "good":
			data = json["data"]
			result = RepositoryPhysicalAsset(self.url, data["UUID"])

			for element in data["classes"]:
				result.classes.append(RepositoryClass(url=self.url,UUID=element))

			for element in data["dataTags"]:
				result.dataTags.append(RepositoryDataTag(self.url, element, UUID))

			for element in data["properties"]:
				result.properties.append(RepositoryProperty(url=self.url,UUID=element["UUID"], value=element["value"]))
			return result
		else:
			raise Exception("error finding physical asset\n"+json["reason"])

	def loadProperty(self, UUID, value=None):
		response = requests.get(self.url+"/api/property/"+UUID)
		json = response.json()

		if(json["status"] == "good"):
			return RepositoryProperty(self.url, json["property"]["UUID"], json["property"]["description"], value)
		else:
			raise Exception("Error finding property")

	def _writeClass(self, VTDRClass):
		requests.post(self.url+"/api/class", json={"UUID":VTDRClass.UUID,"description":VTDRClass.description})

	def _writeDataTag(self, VTDRDataTag):
		requests.post(self.url+"/api/class", json={"UUID":VTDRClass.UUID,"description":VTDRClass.description})

	def _writePhysicalAsset(self, VTDRPhysicalAsset):
		res = requests.post(self.url+"/api/physicalAsset", json={"UUID":VTDRPhysicalAsset.UUID})
		for newClass in VTDRPhysicalAsset.classes:
			res = requests.put(self.url+"/api/physicalAsset/"+VTDRPhysicalAsset.UUID+"/class", json={"class":newClass.UUID})

		for newProperty in VTDRPhysicalAsset.properties:
			res = requests.put(self.url+"/api/physicalAsset/"+VTDRPhysicalAsset.UUID+"/property", json={"property":{"UUID":newProperty.UUID, "value":newProperty.value}})
			print(res.json())

		for newTag in VTDRPhysicalAsset.dataTags:
			res = requests.put(self.url+"/api/physicalAsset/"+VTDRPhysicalAsset.UUID+"/dataTag", json={"dataTag":newTag.UUID})

	def _writeProperty(self, VTDRProperty):
		requests.post(self.url+"/api/property", json={"UUID":VTDRProperty.UUID,"description":VTDRProperty.description})

class RepositoryPhysicalAsset():
	def __init__(self, url, UUID):
		self.url = url
		self.UUID = UUID
		self.classes = []
		self.properties = []
		self.dataTags = []

	def getTag(self, UUID):
		for tag in self.dataTags:
			if tag.UUID == UUID:
				return tag
		raise Exception("Tag does not exist")

	def containsTag(self, UUID):
		for tag in self.dataTags:
			if UUID == tag.UUID:
				return True
		return False

	def setConnectionString(self, connectionString):
		res = requests.put(self.url+"/api/physicalAsset/"+self.UUID+"/primaryAuthentication", json={"primaryAuth":connectionString})
		return res.text

	def createDataTag(self,UUID, dataType):
		if not isinstance(dataType, RepositoryDataType): 
			raise Exception("invalid dataType")
			return
		data = {"tagName":UUID, "UUID":self.UUID+"_"+UUID, "physicalAssetUUID":self.UUID}

		if dataType == RepositoryDataType.STRING:
			data["dataType"]="string"

		if dataType == RepositoryDataType.FLOAT:
			data["dataType"]="float"

		if dataType == RepositoryDataType.INTEGER:
			data["dataType"]="int"

		result = RepositoryDataTag(self.url, UUID, self.UUID)
		res = requests.post(self.url+"/api/dataTag/", json=data)
		self.addDataTag(result, True)
		return result

	def addClass(self, newClass, save=False):
		if isList(newClass):
			for element in newClass:
				self.classes.append(element)
				if save:
					res = requests.put(self.url+"/api/physicalAsset/"+self.UUID+"/class", json={"class":element.UUID})
		else:
			self.classes.append(newClass)
			if save:
				res = requests.put(self.url+"/api/physicalAsset/"+self.UUID+"/class", json={"class":newClass.UUID})

	def addProperty(self, newProperty, save=False):
		if isList(newProperty):
			for element in newProperty:
				self.properties.append(element)
				if save:
					res = requests.put(self.url+"/api/physicalAsset/"+self.UUID+"/property", json={"property":{"UUID":element.UUID, "value":element.value}})
		else:
			self.properties.append(newProperty)
			if save:
				res = requests.put(self.url+"/api/physicalAsset/"+self.UUID+"/property", json={"property":{"UUID":newProperty.UUID, "value":newProperty.value}})
				pass

	def addDataTag(self, newDataTag, save=False):
		if isList(newDataTag):
			for element in newDataTag:
				self.dataTags.append(element)
				if save:
					res = requests.put(self.url+"/api/physicalAsset/"+self.UUID+"/dataTag", json={"dataTag":element.UUID})
		else:
			self.dataTags.append(newDataTag)
			if save:
				res = requests.put(self.url+"/api/physicalAsset/"+self.UUID+"/dataTag", json={"dataTag":newDataTag.UUID})

	def toString(self):
		result = "'"+self.UUID+"'\n"
		result += "\ndataTags:\n"
		for tag in self.dataTags:
			result+= tag.UUID+"\n"

		result += "\nclasses:\n"
		for assetClass in self.classes:
			result+= assetClass.UUID+"\n"

		result += "\nproperties:\n"
		for prop in self.properties:
			result+= prop.UUID+":"+prop.sValue()+"\n"
		return result

	def save(self):
		pass

class RepositoryClass():
	def __init__(self, url, UUID="", description=""):
		self.url = url
		self.UUID = UUID
		self.description = description

class RepositoryProperty():
	def __init__(self, url, UUID="", description="", value=""):
		self.url = url
		self.UUID = UUID
		self.description = description
		self.value = value 

	def setValue(self, value):
		self.value = value

	def sValue(self):
		if self.value is None:
			return "no value"
		else:
			return str(self.value)

class RepositoryDataTag():
	def __init__(self, url, UUID, assetUUID):
		self.UUID = UUID
		self.url = url
		self.assetUUID = assetUUID 

	def pushPoint(self, value, quality=192, time=None):
		timeType = "UTCDate"
		if time is None:
			time = currentTimestamp()
		data = {
			"value":value,
			"time":{
				"value":time,
				"type":timeType
			},
			"quality":quality
		}
		res = requests.put(self.url+"/api/data/"+self.assetUUID+"_"+self.UUID, json=data)
		print(res.text)

class RepositoryDataType(Enum):
	STRING = 0
	INTEGER = 1
	FLOAT = 2
	

def currentTimestamp():
	return datetime.datetime.utcnow().strftime("%Y-%m-%dT%H:%M:%S.%fZ")

