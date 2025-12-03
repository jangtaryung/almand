#! /usr/bin/env python3

from openpyxl import *
import codecs
import csv
import warnings
import os

saveCsvPath = 'resource/'
if not os.path.exists(saveCsvPath) :
	os.makedirs(saveCsvPath)
			
class MyWorkBook :
    def __init__( self, fileName ) :
        warnings.filterwarnings( 'ignore' )
        self.workBook = load_workbook( fileName, data_only=True, read_only= False )
        warnings.resetwarnings()
	
    def ConverSheetToCVS( self, sheetName, startCellPos, cvsName, *excludedCols ) :
        workSheet = self.workBook.get_sheet_by_name( sheetName )
        maxCellPos = workSheet.dimensions.split(':')[1] ;
        selectedRows = workSheet.iter_rows( startCellPos + ':' + maxCellPos )
		
        with codecs.open( saveCsvPath + cvsName + '.csv', 'w', 'utf-8-sig' ) as csvFile :
            csvWriter = csv.writer( csvFile )
            header = [ cell.value for cell in next(selectedRows) ]
            try :
                sliceObj = slice( 0, header.index( None ) )
                header = header[ sliceObj ]
            except :
                sliceObj = None
            excludedColIndinces = [ i for i,x in enumerate(header) if x in excludedCols ]
            #print( header )
            #print( excludedColIndinces )
            csvWriter.writerow( [ name for i,name in enumerate(header) if i not in excludedColIndinces ] )
            
            for row in selectedRows :
                values = [ cell.value for cell in row ]
                if values[0] is None :
                    break
                if sliceObj is not None :
                    values = values[ sliceObj ]
                csvWriter.writerow( [ value for i,value in enumerate(values) if i not in excludedColIndinces ] )
                
        print( 'Converted the Sheet ' + sheetName )            


wb = MyWorkBook( 'table.xlsx' )

wb.ConverSheetToCVS( 'data','A3','csvData',)