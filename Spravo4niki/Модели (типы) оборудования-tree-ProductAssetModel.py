import csv
import xml.etree.ElementTree as ET

fn="1.xml"
fcsv=fn+".csv"
fsql=fn+".sql"

# Create a CSV file and write a header
csv_file = open(fcsv, "w", newline="")
csv_writer = csv.writer(csv_file,delimiter=';')
csv_writer.writerow(
    ["about", "Manufacturer" , "name", "so_AssetTypes"]
)

sql_file = open(fsql, "w", newline="")

tree = ET.parse(fn)
root = tree.getroot()
print ( root)

# get all attributes
attributes = root.attrib
print(attributes)

for book in root.findall("{http://iec.ch/TC57/CIM100#}ProductAssetModel"):
    print("")
    about = book.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}about")
    print(about)
    about = about.replace("-","").replace("#_","")
    cim_IdentifiedObject_name= ""
    cim_Manufacturer = ""
    rdf_resource1 = ""
    so_PSRType_AssetTypes=""
    rdf_resource2 = ""
    for element in book:
        ele_tag = element.tag
        if ele_tag=="{http://so-ups.ru/2015/schema-cim16#}ProductAssetModel.AssetType" :
            so_PSRType_AssetTypes = book.find(ele_tag).text
            rdf_resource2 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource")
            rdf_resource2 = rdf_resource2.replace("-","").replace("#_","")
        if ele_tag=="{http://iec.ch/TC57/CIM100#}ProductAssetModel.Manufacturer" :
            cim_Manufacturer = book.find(ele_tag).text
            rdf_resource1 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource")
            rdf_resource1 = rdf_resource1.replace("-","").replace("#_","")
        if ele_tag=="{http://iec.ch/TC57/CIM100#}IdentifiedObject.name" :
            cim_IdentifiedObject_name = book.find(ele_tag).text
            if cim_IdentifiedObject_name==None :
                cim_IdentifiedObject_name=""
        #print(ele_tag, " : ")
    csv_writer.writerow([about,rdf_resource1,cim_IdentifiedObject_name,rdf_resource2])
    str1="INSERT INTO ProductAssetModel (ABOUT,Manufacturer,NAME,AssetTypes) VALUES ('"+about+"','"+rdf_resource1+"','"+cim_IdentifiedObject_name+"','"+rdf_resource2+"');"
    if rdf_resource2=="" :
        str1="INSERT INTO ProductAssetModel (ABOUT,Manufacturer,NAME,AssetTypes) VALUES ('"+about+"','"+rdf_resource1+"','"+cim_IdentifiedObject_name+"',NULL);"
    sql_file.write(str1+"\n")

# Close the SQL file
sql_file.close()

# Close the CSV file
csv_file.close()


print("Data has been successfully written to "+fcsv)
