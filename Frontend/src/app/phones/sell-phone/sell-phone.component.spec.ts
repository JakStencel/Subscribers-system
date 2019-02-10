import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SellPhoneComponent } from './sell-phone.component';

describe('SellPhoneComponent', () => {
  let component: SellPhoneComponent;
  let fixture: ComponentFixture<SellPhoneComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SellPhoneComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SellPhoneComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
