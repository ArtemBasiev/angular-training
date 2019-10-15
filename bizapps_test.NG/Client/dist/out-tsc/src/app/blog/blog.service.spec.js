import { TestBed } from '@angular/core/testing';
import { BlogService } from './blog.service';
describe('BlogService', () => {
    beforeEach(() => TestBed.configureTestingModule({}));
    it('should be created', () => {
        const service = TestBed.get(BlogService);
        expect(service).toBeTruthy();
    });
});
//# sourceMappingURL=blog.service.spec.js.map