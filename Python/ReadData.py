import pandas
#installeer module met pip install pandas
import os



class Reader:
    def __init__(self,DataDirectory):
        self.Path = os.getcwd() + DataDirectory
    def DataFrame(self,CSVName):
        return pandas.read_csv(self.Path + CSVName)


Reader = Reader("/Data/")
print(Reader.DataFrame("Pokemon.csv"))
# print(Reader.DataFrame("combats.csv"))
# print(Reader.DataFrame("tests.csv"))
