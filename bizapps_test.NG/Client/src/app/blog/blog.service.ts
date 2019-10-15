import { Injectable } from '@angular/core';
import {Blog} from 'src/app/Models/Blog';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import config from 'src/app/config/config.json';
import { map } from 'rxjs/operators';
import { BlogAdapter } from '../Adapters/BlogAdapter';

@Injectable({
  providedIn: 'root'
})
export class BlogService {

  constructor(
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
}
