import csv
import xml.etree.ElementTree as ET

fn="1.xml"
fcsv=fn+".csv"
fsql=fn+".sql"

# Create a CSV file and write a header
csv_file = open(fcsv, "w", newline="")
csv_writer = csv.writer(csv_file,delimiter=';')
csv_writer.writerow(
    ["about", "name", "so_PSRType_AssetTypes"]
)

sql_file = open(fsql, "w", newline="")

tree = ET.parse(fn)
root = tree.getroot()
print ( root)

# get all attributes
attributes = root.attrib
print(attributes)

for book in root.findall("{http://iec.ch/TC57/CIM100#}PSRType"):
    print("")
    about = book.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}about")
    about = about.replace("-","").replace("#_","")
    cim_IdentifiedObject_name= ""
    so_PSRType_AssetTypes=""
    rdf_resource = ""
    for element in book:
        ele_tag = element.tag
        if ele_tag=="{http://so-ups.ru/2015/schema-cim16#}PSRType.AssetTypes" :
            so_PSRType_AssetTypes = book.find(ele_tag).text
            rdf_resource = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource")
            rdf_resource = rdf_resource.replace("-","").replace("#_","")
        if ele_tag=="{http://iec.ch/TC57/CIM100#}IdentifiedObject.name" :
            cim_IdentifiedObject_name = book.find(ele_tag).text
        # print(ele_tag, " : ", ele_val)
    csv_writer.writerow([about,cim_IdentifiedObject_name,rdf_resource])
    str1="INSERT INTO PSRType (ABOUT,NAME,AssetTypes) VALUES ('"+about+"','"+cim_IdentifiedObject_name+"','"+rdf_resource+"');"
    if rdf_resource=="" :
        str1="INSERT INTO PSRType (ABOUT,NAME,AssetTypes) VALUES ('"+about+"','"+cim_IdentifiedObject_name+"',NULL);"
    sql_file.write(str1+"\n")

# Close the SQL file
sql_file.close()

# Close the CSV file
csv_file.close()


print("Data has been successfully written to "+fcsv)
