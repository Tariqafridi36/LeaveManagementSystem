import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { HttpClientModule } from "@angular/common/http";
import { FormsModule } from "@angular/forms";
import { BsDropdownModule } from "ngx-bootstrap";
import { AppComponent } from "./app.component";
import { AuthService } from "./_services/auth.service";
import { ErrorInterceptorProvider } from "./_services/error-interceptor";
import { CoreModule } from "./core/core.module";
import { AppRoutingModule } from "./app-routing.module";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpRequestHeaderProvider } from "./_gaurds/CustomRequestHeader";
import { LoaderInterceptorProvider } from "./_services/loader-interceptor.service";
import { LoaderService } from "./_services/loader/loader.service";
import { LoaderComponent } from "./core/components/loader/loader/loader.component";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { CookieService } from 'ngx-cookie-service'


@NgModule({
  declarations: [
    AppComponent,
    LoaderComponent
  ],
  imports: [
    HttpClientModule,
    CoreModule,
    FormsModule,
    BrowserModule,
    BrowserAnimationsModule,
    BsDropdownModule.forRoot(),
    MatProgressSpinnerModule,
    MatProgressBarModule,
    AppRoutingModule,
  ],
  providers: [
    AuthService,
    LoaderService,
    ErrorInterceptorProvider,
    HttpRequestHeaderProvider,
    LoaderInterceptorProvider,
    CookieService
     
  ],
  bootstrap: [AppComponent],
})
export class AppModule {

}
