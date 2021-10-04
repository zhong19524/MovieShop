import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetfavoriteComponent } from './getfavorite.component';

describe('GetfavoriteComponent', () => {
  let component: GetfavoriteComponent;
  let fixture: ComponentFixture<GetfavoriteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GetfavoriteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GetfavoriteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
