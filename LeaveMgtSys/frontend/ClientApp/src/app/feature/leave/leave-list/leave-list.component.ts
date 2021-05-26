import { SelectionModel } from '@angular/cdk/collections';
import { AfterViewInit, ViewChild } from '@angular/core';
import { Component, OnInit } from '@angular/core'; 
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { NavigationExtras, Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import { LeaveService } from 'src/app/_services/leave/leave.service';
import { CommonService } from 'src/app/_services/shared/common.service';

@Component({
  selector: 'app-leave-list',
  templateUrl: './leave-list.component.html',
  styleUrls: ['./leave-list.component.css']
})
export class LeaveListComponent implements OnInit, AfterViewInit {
  crud: any;
  gridHeight = '244px';
  datasource: any;
  displayedColumn = [];
  pageTitle: any;
  rows = [];
  columns = [
     
    { columnDef: 'id', header: 'ID', width: '10px', cell: (row: any) => `${row.id}`, hide: true },
   
    { columnDef: 'fromDate', header: 'From Date', width: '200px', cell: (row: any) => `${this.common.dateMatFormatter(row.fromDate)}` },
    { columnDef: 'toDate', header: 'To Date', width: '200px', cell: (row: any) => `${this.common.dateMatFormatter(row.toDate)}` },
    { columnDef: 'leaveReason', header: 'Reason', width: '200px', cell: (row: any) => `${row.leaveReason}` },
    { columnDef: 'title', header: 'Details', width: '200px', cell: (row: any) => `${row.title}` },
    { columnDef: 'isApproved', header: 'Status', width: '100px', cell: (row: any) => `${row.isApproved}` },
    { columnDef: 'action', header: 'Action', width: '100px', cell: (row: any) => `${row.action}` }
    
  ];
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  selection = new SelectionModel<any>(false, []);

  constructor(
    private service: LeaveService,
    private alertify: AlertifyService, 
    public common: CommonService, 
    private router: Router,
    public authService: AuthService
  ) { }

  ngOnInit(): void { 
      this.getAll();
    
  }

  onRefersh() {
    this.getAll();
  }

  ngAfterViewInit() {
    this.gridHeight = this.common.gridHeight;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.datasource.filter = filterValue.trim().toLowerCase();
  } 

  getAll() {
    this.service.GetAllLeaves().subscribe((data: any) => {
      if (data.code === '00') {
        
        this.datasource = new MatTableDataSource<any>(data.data);
        this.displayedColumn = this.columns.map(x => x.columnDef); 
        this.datasource.paginator = this.paginator;
        this.datasource.sort = this.sort;

       
      } else {
        this.alertify.error(data.desc);
      }
    }, error => {
      this.alertify.error(error);
    });
  } 

  onEdit(params) {
     
    this.service.form.setValue({
      $key: params.id,
      title: params.title,
      fromdate: params.fromDate,
      todate: params.toDate,
      leavereason: params.leaveReason
    });   
     
     return this.router.navigate(['home/leave/leave-add']);
    
  } 
}
