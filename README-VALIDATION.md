# نتیجهٔ اعتبارسنجی Template

تاریخ: ۲۰۲۶-۱۰-۱۷

## نتیجه

- Pack پروژهٔ Base.Solution.Templates نسخهٔ 10.0.0: موفق.
- بررسی محتوای بسته و نبود bin/obj و فایل‌های IDE: موفق.
- نصب بسته در hive مستقل و تولید راهکار TradingSmoke: موفق.
- تطبیق تمام PackageReferenceهای Base با manifest انتشار ۲۷ بسته‌ای و بررسی SHA256 بسته‌های مبنا: موفق.
- هر ۸ پروژهٔ تولیدشده net10.0 هستند؛ مسیر Infrastructure و نام‌گذاری پروژه‌ها درست است و لایهٔ قدیمی Infra تولید نمی‌شود.
- Restore و Build راهکار تولیدشده در Release: صفر خطا و ۶ هشدار امنیتی NuGet.
- Build و اجرای fixture تست در Debug: موفق؛ ۹ بررسی startup/DI/HTTP پاس شدند.
- تست‌ها ثبت IMediator، اشتراک UnitOfWork، اتصال DbContext فرمان، NoTracking بودن Query DbContext، وجود Controller در OpenAPI، صفحهٔ Scalar، پاسخ POST، فیلتر نتیجهٔ API و نبود مسیر Swagger را بررسی کردند.
- در پروژه‌های تولیدشده هیچ PackageReference مربوط به Swagger/Swashbuckle وجود ندارد.

## محدودیت‌ها و هشدار باقی‌مانده

وابستگی غیرمستقیم System.Security.Cryptography.Pkcs نسخهٔ 9.0.11 هشدارهای NU1902 و NU1903 دارد، از جمله شدت بالا. این وابستگی در این اعتبارسنجی ارتقا داده نشده است. موفقیت ساخت به معنی تأیید امنیتی کامل نیست.

اتصال واقعی SQL Server، اجرای migration و ارتباط با OAuth/سرویس‌های خارجی آزمایش نشدند. تست startup با OAuth غیرفعال انجام شد؛ هیچ migration یا تغییری در دیتابیس اجرا نشد. هیچ NuGet push یا Commit انجام نشد.

سورس Template برای عبور از این بررسی‌ها نیاز به تغییر نداشت. کش و پروژه‌های موقتی ایجادشده در همین نوبت پس از نگهداری گزارش‌ها حذف شدند؛ پوشهٔ خروجی Base تغییر نکرد.

## خروجی و شواهد

- بسته: artifacts/nuget/Base.Solution.Templates.10.0.0.nupkg
- گزارش Build و تولید: artifacts/validation-results/verify.log
- گزارش startup/DI/HTTP: artifacts/validation-results/startup.log
SHA256 بسته: `9569F02A9475C1398C8A443E8DCE4496CB09F180EA6C0DBF05D36BA77662CB97`

## بررسی Build مستقیم سورس Template

پس از تست پروژهٔ تولیدشده، Build مستقیم BaseTemplate/templates/BaseTemplate/BaseTemplate.sln نیز بررسی شد. خطای CS0234 و CS0246 در قرارداد UnitOfWork بازتولید شد: کش عمومی NuGet حاوی بستهٔ قدیمی Base با همان شمارهٔ 10.0.0 بود.

NuGet.Config در ریشهٔ مخزن اضافه شد: Source Mapping برای Base.* به C:\Base\artifacts\branch-nuget و کش ثابت و مستقل artifacts/current-base-packages. سایر وابستگی‌ها از nuget.org دریافت می‌شوند. تنظیمات عمومی NuGet دست‌کاری نشدند. این فایل خارج از محتوای Template است و در بستهٔ نهایی وجود ندارد.

