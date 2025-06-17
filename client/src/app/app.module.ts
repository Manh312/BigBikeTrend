import { NgModule, isDevMode } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from './layout/layout.module';
import { BASE_API } from './core/token/baseUrl.token';
import { environment } from '../environments/environment';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { apiInterceptor } from './core/interceptor/api.interceptor';
import { StoreModule } from '@ngrx/store';
import { store } from './redux/store';
import { EffectsModule } from '@ngrx/effects';
import { CatalogEffects } from './redux/catalog/catalog.effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

@NgModule({
  declarations: [AppComponent, HomeComponent],
  imports: [
    BrowserModule,
    AppRoutingModule,
    LayoutModule,
    BrowserAnimationsModule,
    StoreModule.forRoot(store), // Chỉ một lần gọi
    EffectsModule.forRoot([CatalogEffects]), // Đảm bảo CatalogEffects được liệt kê
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: !isDevMode() }),
  ],
  providers: [
    {
      provide: BASE_API,
      useValue: environment.baseApi,
    },
    provideHttpClient(withInterceptorsFromDi()), // Cấu hình HttpClient mới
    { provide: apiInterceptor, useClass: apiInterceptor, multi: true }, // Inject interceptor
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}