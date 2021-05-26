import { Injectable, Renderer2, Inject } from '@angular/core';
import * as moment from 'moment';
 
 
import { AuthService } from '../auth.service';
import * as XLSX from 'xlsx';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CommonService {


  gridHeight = window.innerHeight - 220 + 'px';
  cardHeight = window.innerHeight - 170 + 'px';
  baseUrl = '';
   

   
   
   
  
  constructor(
    private authService: AuthService,
    private http: HttpClient,
    @Inject('BASE_URL') base: string
  ) {

    if (environment.env === 'local') {
      this.baseUrl = environment.apiUrl + "common/";
    } else {
      this.baseUrl = base + 'api/common/';
    }
  } 
  
  dateFormatter(params) {
    if (params.value) {
      return moment(params.value).format('DD/MM/YYYY hh:mm:ss');
    }
  }

  dateMatFormatter(params) {
    if (params) {
      return moment(params).format('MM/DD/YYYY');
    } else {
      return '-';
    }
  }

  checkForNull(params) {
    return params === 'null' ? '-' : params;
  }
 
   
  exportToExcel(columns: any, rows: any, fileName: string) {
    const xlsHeader = columns.filter(x => x.hide !== true && x.columnDef !== 'action'
      && x.columnDef !== 'status' && x.columnDef !== 'reset' && x.columnDef !== 'delete');
    const createXLSLFormatObj = [];


    createXLSLFormatObj.push(xlsHeader.map(x => x.header));
    rows.data.forEach((item, index) => {
      const innerRow = [];
      for (const col of xlsHeader) {
        innerRow.push(item[col.columnDef]);
      }
      createXLSLFormatObj.push(innerRow);
    });
    console.log(createXLSLFormatObj);
    const wb = XLSX.utils.book_new(),
      ws = XLSX.utils.aoa_to_sheet(createXLSLFormatObj);
    XLSX.utils.book_append_sheet(wb, ws, fileName);
    XLSX.writeFile(wb, fileName);
  } 
}
