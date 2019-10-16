import { Injectable } from '@angular/core';
import {Blog} from 'src/app/Models/Blog';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import config from 'src/app/config/config.json';
import { map } from 'rxjs/operators';
import { BlogAdapter } from '../../Adapters/BlogAdapter';
import { AuthService } from '../auth/auth.service';

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(
    private authService: AuthService,
    private http: HttpClient,
    private adapter: BlogAdapter
  ) { }

  GetBlogByID(blogID:  number | string) :Observable<Blog> {
    let url = config.apiUrl+config.getBlogByIdUrl+blogID;
    let blog = this.http.get<Blog>(url).pipe(
     map(data => this.adapter.adapt(data))
   );
   return blog;
  }

  GetBlogByUserID(userID: number | string) :Observable<Blog>{
    let url = config.apiUrl+config.getBlogByUserIdUrl+userID;
    let blog = this.http.get<Blog>(url).pipe(
     map(data => this.adapter.adapt(data))
   );
   return blog;
  }

  UpdateBlog(blog: Blog) :Observable<boolean> {
    let token = this.authService.GetAuthToken();
    let url = config.apiUrl+config.updateBlogUrl;
    return this.http.post<boolean>(url, blog, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': token
      })
    });
  }

  CreateBlog(blog: Blog): Observable<boolean> {
    let token = this.authService.GetAuthToken();
    let url = config.apiUrl+config.createBlogUrl;
    return this.http.post<boolean>(url, blog, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': token
      })
    });
  }

}
