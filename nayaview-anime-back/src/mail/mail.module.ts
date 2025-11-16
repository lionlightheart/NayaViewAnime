import { Module, Global, DynamicModule } from '@nestjs/common';
import { ConfigModule, ConfigService } from '@nestjs/config';
import Mailgun from 'mailgun.js';
import formData from 'form-data';
import { MAILGUN_CLIENT } from './mail.constants';
import { MailService } from './mail.service';

@Global()
@Module({})
export class MailModule {
  static forRootAsync(): DynamicModule {
    return {
      module: MailModule,
      imports: [ConfigModule],
      providers: [
        {
          provide: MAILGUN_CLIENT,
          inject: [ConfigService],
          useFactory: (config: ConfigService) => {
            const mg = new Mailgun(formData);
            return mg.client({
              username: 'api',
              key: config.get<string>('MAILGUN_API_KEY') || '',
              url:
                config.get<string>('MAILGUN_API_URL') ||
                'https://api.mailgun.net',
            });
          },
        },
        MailService,
      ],
      exports: [MailService],
    };
  }
}
