import { Module, Global, DynamicModule } from '@nestjs/common';
import { ConfigModule, ConfigService } from '@nestjs/config';
import Mailgun from 'mailgun.js';
import formData from 'form-data';
import { MAILGUN_CLIENT } from './mail.constants';
import { MailService } from './mail.service';

// NOTA: si prefieres no exportar globalmente, quita @Global() y la exportación global.
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
            const apiKey = config.get<string>('MAILGUN_API_KEY') || '';
            // mailgun.js requiere form-data cuando usas attachments/MIME
            const mg = new Mailgun(formData);
            const client = mg.client({
              username: 'api',
              key: apiKey,
              // opcional: url: config.get<string>('MAILGUN_API_URL')
            });
            return client;
          },
        },
        MailService,
      ],
      exports: [MailService],
    };
  }
}
