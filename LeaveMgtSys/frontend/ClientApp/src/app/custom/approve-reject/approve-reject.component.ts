import { Component, OnInit, Output, EventEmitter } from '@angular/core';
 

@Component({
  selector: 'app-approve-reject',
  templateUrl: './approve-reject.component.html',
  styleUrls: ['./approve-reject.component.css']
})
export class ApproveRejectComponent implements OnInit {
  params: any;
  @Output() rejectInit: EventEmitter<any> = new EventEmitter<any>();
  constructor( ) {}

  agInit(params: any): void {
    debugger
    this.params = params;
  }

  ngOnInit() {
  }

  approve() {
    this.params.context.componentParent.approve(this.params, this.params.rowIndex);
  }
  reject() {
    this.params.context.componentParent.reject(this.params, this.params.rowIndex);
  }
}
