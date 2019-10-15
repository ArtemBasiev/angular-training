
import {Category} from 'src/app/Models/Category';

export class Post {

  public Id: number;

  public PostTitle: string;

  public PostContent: string;

  public CreationDate: Date;

  public PostCategories: Array<Category> = new Array<Category>();
}
