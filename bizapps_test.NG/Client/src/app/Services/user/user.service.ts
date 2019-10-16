import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { UserAdapter } from 'src/app/Adapters/UserAdapter';
import { User } from 'src/app/Models/User';
import { Observable } from 'rxjs/internal/Observable';
import config from 'src/app/config/config.json';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(
    private http: HttpClient,
    private adapter: UserAdapter
  ) { }

  GetUserByName(userName: string) :Observable<User> {
    let url = config.apiUrl+config.getUserByNameUrl+userName;
    let blog = this.http.get<User>(url).pipe(
     map(data => this.adapter.adapt(data))
   );
   return blog;
  }

}
