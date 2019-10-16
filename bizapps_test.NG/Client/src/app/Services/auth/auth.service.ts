import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import config from 'src/app/config/config.json';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private cookieService: CookieService,
    private http: HttpClient
  ) { }

  login(username: string, password: string) {

    let loginData = 'grant_type=password&username=' + username + '&password=' + password;
    let url = config.apiUrl+config.authorizationServerUrl;

    this.http.post(url, loginData).subscribe((resp: any) => {
      console.log(resp);
      let token =  resp.access_token;
      if((token!=undefined)||(token!=null)){
        this.cookieService.set("token", token, 1);
        this.cookieService.set('user', username, 1);
      }

   });

  }

  logout(): boolean {
    this.cookieService.delete('user');
    this.cookieService.delete('token');
    this.cookieService.delete('userid');
    let userCookieValue = this.cookieService.get('user');
    let tokenCookieValue = this.cookieService.get('token');
    let useridCookieValue = this.cookieService.get('userid');
    if((userCookieValue=="")&&(tokenCookieValue=="")&&(useridCookieValue=="")){
      return true;
    } else {
      return false;
    }
  }

  GetAuthToken(): string {
    let token = this.cookieService.get("token");
    return "Bearer " + token;
  }
  
}
