import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PhysicalAssetsComponent } from './physical-assets.component';

describe('PhysicalAssetsComponent', () => {
  let component: PhysicalAssetsComponent;
  let fixture: ComponentFixture<PhysicalAssetsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PhysicalAssetsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhysicalAssetsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
