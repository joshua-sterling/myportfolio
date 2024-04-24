import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RowingDataComponent } from './rowing-data.component';
import { RowingEventService } from './rowing-data.service';
import { of } from 'rxjs';

describe('RowingDataComponent', () => {
  let component: RowingDataComponent;
  let fixture: ComponentFixture<RowingDataComponent>;
  let rowingEventService: RowingEventService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [RowingDataComponent],
      imports: [HttpClientTestingModule],
      providers: [RowingEventService]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(RowingDataComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should get rowing events on init', () => {
    spyOn(rowingEventService, 'getRowingEvents').and.returnValue(of([]));
    component.ngOnInit();
    expect(rowingEventService.getRowingEvents).toHaveBeenCalled();
  });

  it('should get chart data on init', () => {
    spyOn(rowingEventService, 'getChartData').and.returnValue(of([]));
    component.ngOnInit();
    expect(rowingEventService.getChartData).toHaveBeenCalled();
  });
});
