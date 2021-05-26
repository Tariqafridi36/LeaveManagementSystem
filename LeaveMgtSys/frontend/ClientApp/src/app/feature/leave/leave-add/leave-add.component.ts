import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { LeaveService } from 'src/app/_services/leave/leave.service';
import { CommonService } from 'src/app/_services/shared/common.service';

@Component({
  selector: 'app-leave-add',
  templateUrl: './leave-add.component.html',
  styleUrls: ['./leave-add.component.css']
})
export class LeaveAddComponent implements OnInit {

  gridHeight = '100px';
  reasons:any =  [
        'Sick Leave', 
        'Casual Leave', 
        'Unpaid Leave (or leave without pay)',
        'Paternity leave',
        'Maternity leave'
      ];
  constructor(public service: LeaveService, 
            private alertify: AlertifyService,
             private common: CommonService,
             private router: Router,) { 
              if (!this.service.form.value.$key) { 
                this.service.initializeFormControl();
              }
   }

  ngOnInit(): void {
    if (!this.service.form.value.$key) { 
      this.service.initializeFormControl();
    }
  }
  ngAfterViewInit() {
    this.gridHeight = this.common.cardHeight;
  }

  public hasError = (controlName: string, errorName: string) => {
    return this.service.form.controls[controlName].hasError(errorName);
  }

  save(){
     
    if (this.service.form.value.$key) {
      this.service.form.value.id = this.service.form.value.$key;
      this.service.UpdateLeave(this.service.form.value).subscribe((data:any) => { 
        if (data.code === '00') {
          this.service.initializeFormControl();
          this.alertify.success(data.desc);
        }
      });
    } else {
      this.service.AddLeave(this.service.form.value).subscribe((data:any) => { 
        if (data.code === '00') {
          this.service.initializeFormControl();
          this.alertify.success(data.desc);
        }
      });
    }
   
  }

  OnCancel(){
    this.service.initializeFormControl();
  }

  OnDelete(id: any){
    this.alertify.confirm('Delete','Are you sure want to delete', () => {
      this.service.removeLeave(id).subscribe((data:any) => { 
        if (data.code === '00') {
          this.service.initializeFormControl();
          return this.router.navigate(['home/leave/leave-list']);
        }
      });

    });
   
  }

}
