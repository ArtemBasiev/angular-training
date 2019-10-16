import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/Services/auth/auth.service';
import config from 'src/app/config/config.json';
import { HttpClient } from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { FormBuilder } from '@angular/forms';
import { UserService } from '../Services/user/user.service';
import { User } from '../Models/User';
import { Router } from '@angular/router';

@Component({
  selector: 'app-loginform',
  templateUrl: './loginform.component.html',
  styleUrls: ['./loginform.component.css']
})
export class LoginformComponent implements OnInit {
  loginForm;
  isAuthorized: boolean = false;

  constructor(
    private authService: AuthService,
    private userService: UserService,
    private http: HttpClient,
    private cookieService: CookieService,
    private router: Router,
    private formBuilder: FormBuilder
  ){

    this.loginForm = this.formBuilder.group({
      username: '',
      password: ''
    });
  }

  ngOnInit() {
  }

  login(userLoginData){
    let username = userLoginData.username;
    let password = userLoginData.password;
    let loginData = 'grant_type=password&username=' + username + '&password=' + password;
    let url = config.authorizationServerUrl;

    this.http.post(url, loginData).subscribe((resp: any) => {
      console.log(resp);
      let token =  resp.access_token;
      if((token!=undefined)||(token!=null)){
        this.cookieService.set("token", token, 1);
        this.cookieService.set('user', username, 1);

        let userName = this.cookieService.get("user")
        this.userService.GetUserByName(userName).subscribe((data: User) => {
          let user = { ...data };
          this.cookieService.set("userid", user.Id.toString(), 1);
          this.isAuthorized = true;
        });
      }

   });
  }

  logout(){
    let result = this.authService.logout();
    if(result) this.isAuthorized = false;
    this.router.navigate(['']);
  }
  

}
