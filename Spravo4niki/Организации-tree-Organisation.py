import csv
import xml.etree.ElementTree as ET

fn="1.xml"
fcsv=fn+".csv"
fsql=fn+".sql"

# Create a CSV file and write a header
csv_file = open(fcsv, "w", newline="")
csv_writer = csv.writer(csv_file,delimiter=';')
csv_writer.writerow(
    ["about", "name","Organisation_oGRN", "Organisation_ChildOrganisations","Organisation_Roles"]
)

sql_file = open(fsql, "w", newline="")

tree = ET.parse(fn)
root = tree.getroot()
print ( root)

# get all attributes
attributes = root.attrib
print(attributes)

for book in root.findall("{http://iec.ch/TC57/CIM100#}Organisation"):
    print("")
    about = book.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}about")
    print(about)
    about = about.replace("-","").replace("#_","")

    cim_IdentifiedObject_name= ""
    Organisation_oGRN=""
        
    rdf_resource1 = "" # Organisation.Roles
    rdf_resource2 = "" # Organisation.ChildOrganisations

    for element in book:

        ele_tag = element.tag
        
        if ele_tag=="{http://iec.ch/TC57/CIM100#}Organisation.Roles" :
            rdf1 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource") or ""
            rdf_resource1 = rdf_resource1 + rdf1.replace("#_","").replace("-","") + "\r"
 
        if ele_tag=="{http://iec.ch/TC57/CIM100#}IdentifiedObject.name" :
            cim_IdentifiedObject_name = book.find(ele_tag).text or ""

        if ele_tag=="{http://so-ups.ru/2015/schema-cim16#}Organisation.oGRN" :
            Organisation_oGRN = book.find(ele_tag).text or ""

        if ele_tag=="{http://gost.ru/2019/schema-cim01#}Organisation.ChildOrganisations" :
            rdf2 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource") or ""
            rdf_resource2 = rdf2.replace("#_","").replace("-","")

        #print(ele_tag, " : ")

    cim_IdentifiedObject_name=cim_IdentifiedObject_name.replace("'","")

    csv_writer.writerow(
        [about,cim_IdentifiedObject_name,Organisation_oGRN,rdf_resource2,""]
        )

    str1="INSERT INTO Organisation (ABOUT,NAME,oGRN,ChildOrganisations,Roles) \
          VALUES ('"+about+"','"+cim_IdentifiedObject_name+ "','"+ Organisation_oGRN + "','"+ rdf_resource2 + "','"+  rdf_resource1 + "');"
    
    sql_file.write(str1+"\n")

# Close the SQL file
sql_file.close()

# Close the CSV file
csv_file.close()


print("Data has been successfully written to "+fcsv)
