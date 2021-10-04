import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GetpurchaseComponent } from './getpurchase.component';

describe('GetpurchaseComponent', () => {
  let component: GetpurchaseComponent;
  let fixture: ComponentFixture<GetpurchaseComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GetpurchaseComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GetpurchaseComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
