import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RowingDataComponent } from './rowing-data.component';

describe('RowingDataComponent', () => {
  let component: RowingDataComponent;
  let fixture: ComponentFixture<RowingDataComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RowingDataComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RowingDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
