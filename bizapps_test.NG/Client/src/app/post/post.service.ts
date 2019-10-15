import { Injectable } from '@angular/core';
import {Post} from 'src/app/Models/Post';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import config from 'src/app/config/config.json';
import { map } from 'rxjs/operators';
import { PostAdapter } from '../Adapters/PostAdapter';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  constructor(
    private http: HttpClient,
    private adapter: PostAdapter
    ) { }

  GetPostByID(postID:  number | string) :Observable<Post> {
    let url = config.apiUrl+config.getPostByIdUrl+postID;
   let post = this.http.get<Post>(url).pipe(
     map(data => this.adapter.adapt(data))
   );
   return (post as Observable<Post>);
  }
}



