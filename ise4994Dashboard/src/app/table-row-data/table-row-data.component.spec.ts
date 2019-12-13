import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TableRowDataComponent } from './table-row-data.component';

describe('TableRowDataComponent', () => {
  let component: TableRowDataComponent;
  let fixture: ComponentFixture<TableRowDataComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TableRowDataComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TableRowDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
