import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ProcessListComponent } from './components/process-list/process-list.component';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,
    ProcessListComponent,
    HttpClientModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'system-monitor-ui';
}
