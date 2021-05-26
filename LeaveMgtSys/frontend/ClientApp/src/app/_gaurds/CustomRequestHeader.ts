import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HTTP_INTERCEPTORS, HttpXsrfTokenExtractor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { publicKey } from './config';
 
import * as forge from 'node-forge';  
 

@Injectable()
export class CustomRequestHeader implements HttpInterceptor {
  $encrypt: any;
  constructor() {
     
  }
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('token');
    const encryptedBody = this.encrypt(req);


    let request = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      },
      body: encryptedBody
    });
    // encrypted request
    const data = next.handle(request); 

    // plan request
    // const data = next.handle(req.clone({
    //   setHeaders: {
    //     Authorization: `Bearer ${token}`
    //   }
    // }));
    return data;
  } 
 
  encrypt(req) { 
    try {   
        var rsa = forge.pki.publicKeyFromPem(publicKey);   
        return window.btoa(rsa.encrypt(JSON.stringify(req.body))); 

    } catch (error) {
      console.log(error);
    }
  }
}

export const HttpRequestHeaderProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: CustomRequestHeader,
  multi: true
};
