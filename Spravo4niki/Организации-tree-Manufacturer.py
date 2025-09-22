import csv
import xml.etree.ElementTree as ET

fn="P:\\EmaTcl\\1.xml"
fcsv=fn+".csv"
fsql=fn+".sql"

# Create a CSV file and write a header
csv_file = open(fcsv, "w", newline="")
csv_writer = csv.writer(csv_file,delimiter=';')
csv_writer.writerow(
    ["about", "name","OrganisationRoleOrganisation"]
)

sql_file = open(fsql, "w", newline="")

tree = ET.parse(fn)
root = tree.getroot()
print ( root)

# get all attributes
attributes = root.attrib
print(attributes)

for book in root.findall("{http://iec.ch/TC57/CIM100#}Manufacturer"):
    print("")
    about = book.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}about")
    print(about)
    about = about.replace("-","").replace("#_","")

    cim_IdentifiedObject_name= ""
        
    rdf_resource1 = "" # OrganisationRole_Organisation

    for element in book:

        ele_tag = element.tag
        
        if ele_tag=="{http://iec.ch/TC57/CIM100#}IdentifiedObject.name" :
            cim_IdentifiedObject_name = book.find(ele_tag).text or ""

        if ele_tag=="{http://iec.ch/TC57/CIM100#}OrganisationRole.Organisation" :
            rdf2 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource") or ""
            rdf_resource2 = rdf2.replace("#_","").replace("-","")

        #print(ele_tag, " : ")

    cim_IdentifiedObject_name=cim_IdentifiedObject_name.replace("'","")

    csv_writer.writerow(
        [about,cim_IdentifiedObject_name,rdf_resource2]
        )

    str1="INSERT INTO Manufacturer (ABOUT,NAME,OrganisationRoleOrganisation) \
          VALUES ('"+about+"','"+cim_IdentifiedObject_name+ "','"+ rdf_resource2 + "');"
    
    sql_file.write(str1+"\n")

# Close the SQL file
sql_file.close()

# Close the CSV file
csv_file.close()


print("Data has been successfully written to "+fcsv)
