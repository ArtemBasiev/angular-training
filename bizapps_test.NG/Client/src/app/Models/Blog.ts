
import {Category} from 'src/app/Models/Category';
import { Post } from './Post';

export class Blog {

  public Id: number;

  public BlogTitle: string;

  public CreationDate: Date;

  public BlogCategories: Array<Category> = new Array<Category>();

  public BlogPosts: Array<Post> = new Array<Post>();
}