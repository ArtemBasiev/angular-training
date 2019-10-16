import { Injectable } from '@angular/core';
import { IAdapter } from './IAdapter';
import { User } from '../Models/User';


@Injectable({
    providedIn: 'root'
})
export class UserAdapter implements IAdapter<User> {

  adapt(item: any): User {
    if(item==null) return null;

    var user = new User();
    user.Id = item.Id;
    user.UserName = item.UserName;
    user.UserPassword = item.UserPassword;
    return user;
  }
}