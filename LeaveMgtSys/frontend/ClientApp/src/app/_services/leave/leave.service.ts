import { Injectable, Inject } from "@angular/core";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { HttpClient } from "@angular/common/http";
import { environment } from "src/environments/environment";
 

@Injectable({
  providedIn: "root",
})
export class LeaveService {
 
  baseUrl = "";
  buttonText = "Save";
   
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') base: string
  ) {
    if (environment.env === 'local') {
      this.baseUrl = environment.apiUrl + "Leave/";
    } else {
      this.baseUrl = base + "api/leave/";
    }
  }

  form: FormGroup = new FormGroup({
    $key: new FormControl(null),
    title: new FormControl("", [Validators.required, Validators.maxLength(50)]),
    leavereason: new FormControl("", [Validators.required, Validators.maxLength(50)]),
    fromdate: new FormControl("", Validators.required),
    todate:  new FormControl("", Validators.required)
 
     
  });

  initializeFormControl() {
    this.form.setValue({
      $key: null,
      title: "",
      leavereason: null,
      fromdate: new Date(),
      todate: new Date()
      
    });
  }

  populateForm(configData) {
    this.buttonText = "Update";
    this.form.setValue(configData);
  }
  

  AddLeave(model: any) { 
    return this.http.post(this.baseUrl + "addLeave", model);
  }

  GetAllLeaves(){
    return this.http.get(this.baseUrl + "GetAllLeaves");
  }

  UpdateLeave(model: any) { 
    return this.http.post(this.baseUrl + "UpdateLeave", model);
  } 

  removeLeave(id: any) {
    return this.http.delete(this.baseUrl + id);
  }

  GetUserLeaveForDashboard(){
    return this.http.get(this.baseUrl + "GetUserLeaveForDashboard");
  }
}
