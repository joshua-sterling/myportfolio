import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AboutMeComponent } from './about-me.component';

describe('AboutMeComponent', () => {
  let component: AboutMeComponent;
  let fixture: ComponentFixture<AboutMeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AboutMeComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AboutMeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should cycle to the next image', () => {
    component.currentImageIndex = 0;
    component.nextImage();
    expect(component.currentImageIndex).toEqual(1);
  });

  it('should cycle to the previous image', () => {
    component.currentImageIndex = 1;
    component.prevImage();
    expect(component.currentImageIndex).toEqual(0);
  });

  it('should cycle to the last image when on the first image and prevImage is called', () => {
    component.currentImageIndex = 0;
    component.prevImage();
    expect(component.currentImageIndex).toEqual(component.images.length - 1);
  });

  it('should cycle back to the first image when at the last image and nextImage is called', () => {
    component.currentImageIndex = component.images.length - 1;
    component.nextImage();
    expect(component.currentImageIndex).toEqual(0);
  });
});
