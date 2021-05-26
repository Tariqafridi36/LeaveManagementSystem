import { Component, OnInit } from '@angular/core';
import { CommonService } from 'src/app/_services/shared/common.service';
import { ChartDataSets, ChartOptions, ChartType } from 'chart.js';
import { Color, Label, MultiDataSet } from 'ng2-charts';
import { LeaveService } from 'src/app/_services/leave/leave.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  gridHeight = '224px';
  constructor(private common: CommonService, private service: LeaveService) { }

  ngOnInit(): void {
    this.populateChart();
  }

  ngAfterViewInit() {
    this.gridHeight = this.common.cardHeight;
  }
  leaveData:any;
  total = 30;
  consume: number = 0;
  doughnutChartLabels: Label[] = [];
  doughnutChartData: MultiDataSet = [];
  doughnutChartType: ChartType = 'doughnut'; 
  populateChart(){
    this.service.GetUserLeaveForDashboard().subscribe((data: any) => {
      debugger
     this.leaveData = data.data;
      data.data.forEach((row) => {
       
        this.doughnutChartLabels.push(row.reason.toString());
        this.doughnutChartData.push(row.count.toString());
        this.consume += row.count;
      }); 
    });
  }

}
