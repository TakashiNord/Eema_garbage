import csv
import xml.etree.ElementTree as ET

fn="1.xml"
fcsv=fn+".csv"
fsql=fn+".sql"

# Create a CSV file and write a header
csv_file = open(fcsv, "w", newline="")
csv_writer = csv.writer(csv_file,delimiter=';')
csv_writer.writerow(
    ["about", "name", "sizeDescription", "material", "radius",
     "diameterOverJacket","diameterOverCore","diameterOverInsulation",
     "tapeThickness","sheathThickness","constructionKind","outerJacketKind",
     "insulationEr","insulationErShield","shieldMaterial","shieldIsTransposed","shieldCrossSection",
     "shieldGrounding","crossSection","underShieldScreenThickness","radialMoistureBarrierThicknes"]
)

sql_file = open(fsql, "w", newline="")

tree = ET.parse(fn)
root = tree.getroot()
print ( root)

# get all attributes
attributes = root.attrib
print(attributes)

for book in root.findall("{http://iec.ch/TC57/CIM100#}TapeShieldCableInfo"):
    print("")
    about = book.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}about")
    print(about)
    about = about.replace("-","").replace("#_","")

    cim_IdentifiedObject_name= ""
        
    sizeDescription=""
    ratedCurrent=""
    rDC20=""
    material=""
    shieldMaterial=""
    diameterOverJacket=""
    diameterOverCore=""
    diameterOverInsulation=""
    radius=""
    rdf_resource1 = "" # material
    rdf_resource2 = "" # constructionKind
    rdf_resource4 = "" # shieldMaterial
    rdf_resource5 = "" # outerJacketKind
    constructionKind=""
    nominalTemperature=""
    tapeThickness=""

    insulationEr=""
    insulationErShield=""
    shieldIsTransposed=""
    shieldCrossSection=""
    rdf_resource3 = "" # shieldGrounding
    crossSection=""
    underShieldScreenThickness=""
    radialMoistureBarrierThicknes=""
    sheathThickness=""

    for element in book:

        ele_tag = element.tag
        
        if ele_tag=="{http://iec.ch/TC57/CIM100#}WireInfo.sizeDescription" :
            sizeDescription = book.find(ele_tag).text or ""
        if ele_tag=="{http://iec.ch/TC57/CIM100#}WireInfo.material" :
            material = book.find(ele_tag).text or ""
            rdf_resource1 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource") or ""
            rdf_resource1 = rdf_resource1.replace("cim:","")
        if ele_tag=="{http://iec.ch/TC57/CIM100#}CableInfo.shieldMaterial" :
            shieldMaterial = book.find(ele_tag).text or ""
            rdf_resource4 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource") or ""
            rdf_resource4 = rdf_resource1.replace("cim:","")
        if ele_tag=="{http://iec.ch/TC57/CIM100#}CableInfo.diameterOverJacket" :
            diameterOverJacket = book.find(ele_tag).text or ""
        if ele_tag=="{http://iec.ch/TC57/CIM100#}WireInfo.radius" :
            radius = book.find(ele_tag).text or ""
        if ele_tag=="{http://iec.ch/TC57/CIM100#}TapeShieldCableInfo.tapeThickness" :
            tapeThickness = book.find(ele_tag).text or ""
        if ele_tag=="{http://iec.ch/TC57/CIM100#}CableInfo.constructionKind" :
            constructionKind = book.find(ele_tag).text or ""
            rdf_resource2 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource") or ""
            rdf_resource2 = rdf_resource1.replace("cim:","")
        if ele_tag=="{http://iec.ch/TC57/CIM100#}CableInfo.outerJacketKind" :
            outerJacketKind = book.find(ele_tag).text or ""
            rdf_resource5 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource") or ""
            rdf_resource5 = rdf_resource1.replace("cim:","")
        if ele_tag=="{http://iec.ch/TC57/CIM100#}CableInfo.diameterOverCore" :
            diameterOverCore = book.find(ele_tag).text or ""
        if ele_tag=="{http://iec.ch/TC57/CIM100#}CableInfo.nominalTemperature" :
            nominalTemperature = book.find(ele_tag).text or ""
        if ele_tag=="{http://iec.ch/TC57/CIM100#}CableInfo.diameterOverInsulation" :
            diameterOverInsulation = book.find(ele_tag).text or ""

        if ele_tag=="{http://gost.ru/2019/schema-cim01#}CableInfo.insulationEr" :
            insulationEr = book.find(ele_tag).text or ""
        if ele_tag=="{http://gost.ru/2019/schema-cim01#}CableInfo.insulationErShield" :
            insulationErShield = book.find(ele_tag).text or "" 
        if ele_tag=="{http://gost.ru/2019/schema-cim01#}CableInfo.shieldIsTransposed" :
            shieldIsTransposed = book.find(ele_tag).text or ""
        if ele_tag=="{http://gost.ru/2019/schema-cim01#}CableInfo.radialMoistureBarrierThicknes" :
            radialMoistureBarrierThicknes = book.find(ele_tag).text or ""
        if ele_tag=="{http://gost.ru/2019/schema-cim01#}CableInfo.shieldCrossSection" :
            shieldCrossSection = book.find(ele_tag).text or ""
        if ele_tag=="{http://gost.ru/2019/schema-cim01#}WireInfo.crossSection" :
            crossSection = book.find(ele_tag).text or ""
        if ele_tag=="{http://gost.ru/2019/schema-cim01#}CableInfo.underShieldScreenThickness" :
            underShieldScreenThickness = book.find(ele_tag).text or ""
        if ele_tag=="{http://gost.ru/2019/schema-cim01#}CableInfo.shieldGrounding" :
            shieldGrounding = book.find(ele_tag).text or ""
            rdf_resource3 = element.get("{http://www.w3.org/1999/02/22-rdf-syntax-ns#}resource") or ""
            rdf_resource3 = rdf_resource1.replace("rf:","")            
        if ele_tag=="{http://gost.ru/2019/schema-cim01#}CableInfo.sheathThickness" :
            sheathThickness = book.find(ele_tag).text or ""

        if ele_tag=="{http://iec.ch/TC57/CIM100#}IdentifiedObject.name" :
            cim_IdentifiedObject_name = book.find(ele_tag).text or ""
        #print(ele_tag, " : ")
    csv_writer.writerow(
        [about,cim_IdentifiedObject_name,sizeDescription,rdf_resource1,radius,
         diameterOverJacket,diameterOverCore,diameterOverInsulation,
         tapeThickness,sheathThickness,rdf_resource2,rdf_resource5,
         insulationEr,insulationErShield,rdf_resource4,shieldIsTransposed,shieldCrossSection,
         rdf_resource3,crossSection,underShieldScreenThickness,radialMoistureBarrierThicknes]
        )

    str1="INSERT INTO TapeShieldCableInfo (ABOUT,NAME,sizeDescription,material,radius,diameterOverJacket,diameterOverCore, \
diameterOverInsulation,tapeThickness,sheathThickness,constructionKind,outerJacketKind, \
insulationEr,insulationErShield,shieldMaterial,shieldIsTransposed,shieldCrossSection, \
shieldGrounding,crossSection,underShieldScreenThickness,radialMoistureBarrierThicknes ) \
          VALUES ('"+about+"','"+cim_IdentifiedObject_name+"','"+sizeDescription+"','"+rdf_resource1+"','"+radius+"','"+ \
    diameterOverJacket+"','"+diameterOverCore+"','"+diameterOverInsulation+"','"+ \
    tapeThickness+"','"+sheathThickness+"','"+rdf_resource2+"','"+rdf_resource5+"','"+ \
    insulationEr+"','"+insulationErShield+"','"+rdf_resource4+"','"+shieldIsTransposed+"','"+shieldCrossSection+"','"+ \
    rdf_resource3+"','"+crossSection+"','"+underShieldScreenThickness+"','"+radialMoistureBarrierThicknes + \
    "');"
    
    sql_file.write(str1+"\n")

# Close the SQL file
sql_file.close()

# Close the CSV file
csv_file.close()


print("Data has been successfully written to "+fcsv)
