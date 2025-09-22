import csv
import xml.etree.ElementTree as ET

fn="1.xml"
fcsv=fn+".csv"
fsql=fn+".sql"

# Create a CSV file and write a header
csv_file = open(fcsv, "w", newline="")
csv_writer = csv.writer(csv_file,delimiter=';')
csv_writer.writerow(
    ["about", "name", "sizeDescription", "ratedCurrent", "rDC20", "material", "radius"]
)

sql_file = open(fsql, "w", newline="")

tree = ET.parse(fn)
root = tree.getroot()
print ( root)

# get all attributes
attributes = root.attrib
print(attributes)

for book in root.findall("{http://iec.ch/TC57/CIM100#}OverheadWireInfo"):
    print("")
    about = book.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}about")
    print(about)
    about = about.replace("-","").replace("#_","")
    cim_IdentifiedObject_name= ""
    sizeDescription=""
    ratedCurrent=""
    rDC20=""
    material=""
    radius=""
    rdf_resource1 = ""
    for element in book:
        ele_tag = element.tag
        if ele_tag=="{http://iec.ch/TC57/CIM100#}WireInfo.sizeDescription" :
            sizeDescription = book.find(ele_tag).text or ""
        if ele_tag=="{http://iec.ch/TC57/CIM100#}WireInfo.ratedCurrent" :
            ratedCurrent = book.find(ele_tag).text or ""
        if ele_tag=="{http://iec.ch/TC57/CIM100#}WireInfo.rDC20" :
            rDC20 = book.find(ele_tag).text or ""
        if ele_tag=="{http://iec.ch/TC57/CIM100#}WireInfo.radius" :
            radius = book.find(ele_tag).text or ""
        if ele_tag=="{http://iec.ch/TC57/CIM100#}WireInfo.material" :
            material = book.find(ele_tag).text or ""
            rdf_resource1 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource") or ""
            rdf_resource1 = rdf_resource1.replace("cim:","")
        if ele_tag=="{http://iec.ch/TC57/CIM100#}IdentifiedObject.name" :
            cim_IdentifiedObject_name = book.find(ele_tag).text or ""
        #print(ele_tag, " : ")
    csv_writer.writerow([about,cim_IdentifiedObject_name,sizeDescription,ratedCurrent,rDC20,rdf_resource1,radius])
    str1="INSERT INTO OverheadWireInfo (ABOUT,NAME,sizeDescription,ratedCurrent,rDC20,material,radius) \
          VALUES ('"+about+"','"+cim_IdentifiedObject_name+"','"+sizeDescription+"','"+ratedCurrent+"','"+rDC20+"','"+rdf_resource1+"','"+radius+"');"
    sql_file.write(str1+"\n")

# Close the SQL file
sql_file.close()

# Close the CSV file
csv_file.close()


print("Data has been successfully written to "+fcsv)
