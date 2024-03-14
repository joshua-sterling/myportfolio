import { HttpClient } from '@angular/common/http';
import { Component, OnInit, HostListener, ElementRef } from '@angular/core';

interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  
  @HostListener('window:mousemove', ['$event'])
  onMouseMove(event: MouseEvent) {
    const flareElement = this.elRef.nativeElement.querySelector('.flare');
    const bannerElement = this.elRef.nativeElement.querySelector('.banner');
    const rect = bannerElement.getBoundingClientRect();

    // Only move the lens flare if the mouse is within the banner, otherwise it is very distracting
    if (event.clientX > rect.left && event.clientX < rect.right && event.clientY > rect.top && event.clientY < rect.bottom) {
      flareElement.style.transform = `translateX(${event.clientX - rect.left}px) rotate(${(event.clientX - rect.left) / 10 - 20}deg)`;
    }
  }
  constructor(private http: HttpClient, private elRef: ElementRef) {}

  ngOnInit() {
  }

  

  title = 'myportfolio.client';
}
