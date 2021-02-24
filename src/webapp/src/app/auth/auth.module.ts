import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FlexLayoutModule } from '@angular/flex-layout';

import { LoginComponent } from './login/login.component';
import { AuthRoutingModule } from './auth-routing.module';
import { MaterialModule } from '../shared/modules/material/material.module';
import { AuthenticationService } from './auth.service';
import { ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';

@NgModule({
    imports: [
        CommonModule,
        AuthRoutingModule,
        MaterialModule,
        ReactiveFormsModule,
        FlexLayoutModule.withConfig({ addFlexToParent: false })
    ],
    declarations: [LoginComponent, RegisterComponent]
})
export class AuthModule { }
