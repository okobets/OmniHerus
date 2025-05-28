import { HttpClient } from '@angular/common/http';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

export interface WeatherForecast {
  date: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: false,
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit, OnDestroy {
  public forecasts: WeatherForecast[] = [];
  private destroy$ = new Subject<void>();

  constructor(private readonly http: HttpClient) {}

  ngOnInit() {
    this.getForecasts();
  }

  ngOnDestroy() {
    this.destroy$.next();
    this.destroy$.complete();
  }

  getForecasts() {
    this.http.get<WeatherForecast[]>('/weatherforecast')
      .pipe(takeUntil(this.destroy$))
      .subscribe({
        next: (result) => {
          this.forecasts = result;
        },
        error: (err) => {
          // Handle error (log or show message)
          console.error('Failed to load forecasts', err);
        }
      });
  }

  title = 'web.client';
}
