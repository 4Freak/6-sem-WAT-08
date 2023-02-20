using Newtonsoft.Json.Bson;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V108.Network;
using OpenQA.Selenium.Firefox;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace _08
{
	public class Tests
	{

		// SetUP
		private FirefoxDriverService _service;
		private FirefoxOptions _options;

		// Test A
		private By _txtMenu = By.XPath("//*[contains(text(),'menu')]");
		private By _txtBanners = By.XPath("//*[contains(text(),'banners')]");

		// Test B
		private By _lastTableElement = By.XPath("//table//tr[last()]//td[last()][contains(text(),'CoolSoft by Somebody')]");

		// Test C
		private By _txtFieldName = By.XPath("//input[@name='name']");
		private By _txtFieldHeight = By.XPath("//input[@name='height']");
		private By _txtFieldWeight = By.XPath("//input[@name='weight']");
		private By _rbMale = By.XPath("//input[@name='gender' and @value='m']");
		private By _rbFemale = By.XPath("//input[@name='gender' and @value='f']");

		// Test D
		// (using Test C fields)
		private By _btnCalculate = By.XPath("//input[@type='submit' and @value='Рассчитать']");
		private By _txtTooBigWeight = By.XPath("//*[text()='Слишком большая масса тела']");

		// Test E
		private By _allTxtFields = By.XPath("//input[@type='text']");
		private By _allRbs = By.XPath("//input[@type='radio']");
		private By _allBtns = By.XPath("//input[@type='submit']");

		// Test F
		// (using Text C fields)
		private By _txtErrorHeight = By.XPath("//*[text()='Рост должен быть в диапазоне 50-300 см.']");
		private By _txtErrorWeight = By.XPath("//*[text()='Вес должен быть в диапазоне 3-500 кг.']");

		// Test G
		private By _txtDate = By.XPath("//table//*[contains(text(), 'Расчёт веса')]");

		[SetUp]
		public void Setup ()
		{
			_service = FirefoxDriverService.CreateDefaultService ("d:\\BSUIR\\6_Sem\\WAT\\Projects\\_08\\_08\\bin\\Debug\\net6.0", "geckodriver.exe");
			_service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox\firefox.exe";
			_options = new FirefoxOptions ();
		}

		[Test]
		public void  TestA()
		{
			var options = _options;
			options.AddArgument("-headless");
			var webDriver = new FirefoxDriver(_service, options);
			webDriver.Navigate().GoToUrl ("http://svyatoslav.biz/testlab/wt/");
			try
			{
				var txtMenu = webDriver.FindElement(_txtMenu);
				var txtBanners = webDriver.FindElement(_txtBanners);

				Assert.Multiple( () => {
					Assert.That(txtMenu, Is.Not.Null);
					Assert.That(txtBanners, Is.Not.Null);
				});
			}
			catch (OpenQA.Selenium.NoSuchElementException ex)
			{
				Assert.Fail();
			}
			finally
			{
				webDriver.Quit();
			}
		}

		[Test]
		public void TestB()
		{
			var options = _options;
			options.AddArgument("-headless");
			var webDriver = new FirefoxDriver(_service, options);
			webDriver.Navigate().GoToUrl ("http://svyatoslav.biz/testlab/wt/");
			try
			{
				var lastTableElement = webDriver.FindElement(_lastTableElement);
				
				Assert.That(lastTableElement, Is.Not.Null);
			}
			catch (OpenQA.Selenium.NoSuchElementException ex)
			{
				Assert.Fail();
			}
			finally
			{
				webDriver.Quit();
			}
		}

		[Test]
		public void TestC()
		{
			var options = _options;
			options.AddArgument("-headless");
			var webDriver = new FirefoxDriver(_service, options);
			webDriver.Navigate().GoToUrl ("http://svyatoslav.biz/testlab/wt/");
			try
			{
				IWebElement txtFieldName = (IWebElement)webDriver.FindElement(_txtFieldName);
				IWebElement txtFieldHeight = (IWebElement)webDriver.FindElement(_txtFieldHeight);
				IWebElement txtFieldWeight = (IWebElement)webDriver.FindElement(_txtFieldWeight);

				Assert.Multiple(() => {
					Assert.That(txtFieldName.GetAttribute("value"), Is.EqualTo(""));
					Assert.That(txtFieldHeight.GetAttribute("value"), Is.EqualTo(""));
					Assert.That(txtFieldWeight.GetAttribute("value"), Is.EqualTo(""));
				});

				IWebElement rbMale = (IWebElement)webDriver.FindElement(_rbMale);
				IWebElement rbFemale = (IWebElement)webDriver.FindElement(_rbFemale);

				Assert.Multiple(() => {
					Assert.That(rbMale.Selected, Is.EqualTo(false));
					Assert.That(rbFemale.Selected, Is.EqualTo(false));
				});
			}
			catch (OpenQA.Selenium.NoSuchElementException ex)
			{
				Assert.Fail();
			}
			finally
			{
				webDriver.Quit();
			}	
		}

		[Test]
		public void TestD()
		{
			var options = _options;
			options.AddArgument("-headless");
			var webDriver = new FirefoxDriver(_service, options);
			webDriver.Navigate().GoToUrl ("http://svyatoslav.biz/testlab/wt/");
			try
			{
				IWebElement txtFieldName = (IWebElement)webDriver.FindElement(_txtFieldName);
				IWebElement txtFieldHeight = (IWebElement)webDriver.FindElement(_txtFieldHeight);
				IWebElement txtFieldWeight = (IWebElement)webDriver.FindElement(_txtFieldWeight);

				txtFieldName.SendKeys("Test User");
				txtFieldHeight.SendKeys("50");
				txtFieldWeight.SendKeys("3");

				IWebElement rbMale = (IWebElement)webDriver.FindElement(_rbMale);

				rbMale.Click();

				IWebElement btnCalculate = (IWebElement)webDriver.FindElement(_btnCalculate);
				
				btnCalculate.Click();

				IWebElement txtTooBigWeight = (IWebElement)webDriver.FindElement(_txtTooBigWeight);

			}
			catch (OpenQA.Selenium.NoSuchElementException ex)
			{
				Assert.Fail();
			}
			finally
			{
				webDriver.Quit();
			}			
		}

		[Test]
		public void TestE()
		{
			var options = _options;
			options.AddArgument("-headless");
			var webDriver = new FirefoxDriver(_service, options);
			webDriver.Navigate().GoToUrl ("http://svyatoslav.biz/testlab/wt/");
			try
			{
				var allTxtFields = webDriver.FindElements(_allTxtFields);
				
				Assert.That(allTxtFields.Count, Is.EqualTo(3));

				var allRbs = webDriver.FindElements(_allRbs);

				Assert.Multiple( () => {
					Assert.That(allRbs.Count, Is.EqualTo(2));
					Assert.That(allRbs[0].GetAttribute("name"), Is.EqualTo(allRbs[1].GetAttribute("name")));
				});

				var allBtns = webDriver.FindElements(_allBtns);

				Assert.That(allBtns.Count, Is.EqualTo(1));
			}
			catch (OpenQA.Selenium.NoSuchElementException ex)
			{
				Assert.Fail();
			}
			finally
			{
				webDriver.Quit();
			}			
		}

		[Test]
		public void TestF()
		{
			var options = _options;
			options.AddArgument("-headless");
			var webDriver = new FirefoxDriver(_service, options);
			webDriver.Navigate().GoToUrl ("http://svyatoslav.biz/testlab/wt/");
			try
			{
				// Assert Error Height and Error Weight
				IWebElement txtFieldHeight = (IWebElement)webDriver.FindElement(_txtFieldHeight);
				IWebElement txtFieldWeight = (IWebElement)webDriver.FindElement(_txtFieldWeight);

				txtFieldHeight.SendKeys("0");
				txtFieldWeight.SendKeys("0");

				IWebElement btnCalculate = (IWebElement)webDriver.FindElement(_btnCalculate);
				
				btnCalculate.Click();

				var txtErrorHeight = webDriver.FindElement(_txtErrorHeight);
				var txtErrorWeight = webDriver.FindElement(_txtErrorWeight);

				Assert.Multiple( () => {
					Assert.That(txtErrorHeight, Is.Not.Null);
					Assert.That(txtErrorWeight, Is.Not.Null);
				});

				// Assert Error Height
				txtFieldHeight = (IWebElement)webDriver.FindElement(_txtFieldHeight);
				txtFieldWeight = (IWebElement)webDriver.FindElement(_txtFieldWeight);

				txtFieldHeight.SendKeys("0");
				txtFieldWeight.SendKeys("50");

				txtErrorHeight = webDriver.FindElement(_txtErrorHeight);
				
				Assert.That(txtErrorHeight, Is.Not.Null);

				// Assert Error Weight
				txtFieldHeight = (IWebElement)webDriver.FindElement(_txtFieldHeight);
				txtFieldWeight = (IWebElement)webDriver.FindElement(_txtFieldWeight);

				txtFieldHeight.SendKeys("50");
				txtFieldWeight.SendKeys("0");

				txtErrorWeight = webDriver.FindElement(_txtErrorWeight);
				
				Assert.That(txtErrorWeight, Is.Not.Null);
			}
			catch (OpenQA.Selenium.NoSuchElementException ex)
			{
				Assert.Fail();
			}
			finally
			{
				webDriver.Quit();
			}			
		}

		[Test]
		public void TestG()
		{
			var options = _options;
			options.AddArgument("-headless");
			var webDriver = new FirefoxDriver(_service, options);
			webDriver.Navigate().GoToUrl ("http://svyatoslav.biz/testlab/wt/");
			try
			{
				IWebElement txtDate = (IWebElement)webDriver.FindElement(_txtDate);
				int startDateId = 0, i = 0;
				while (i < txtDate.Text.Length && startDateId == 0)
				{
					if (txtDate.Text[i] >= '0' && txtDate.Text[i] <= '9')
						startDateId = i;
					
					i++;
				}
				string date = txtDate.Text.Substring(startDateId);
				
				DateTime resultDateTime;
				string format = "dd.MM.yyyy";
				if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out resultDateTime))
				{
					DateTime currDateTime = DateTime.Now;
					Assert.Multiple( () => {
						Assert.That(currDateTime.Day, Is.EqualTo(resultDateTime.Day));
						Assert.That(currDateTime.Month, Is.EqualTo(resultDateTime.Month));
						Assert.That(currDateTime.Year, Is.EqualTo(resultDateTime.Year));
					});
				}
				else
					Assert.Fail();
			}
			catch (OpenQA.Selenium.NoSuchElementException ex)
			{
				Assert.Fail();
			}
			finally
			{
				webDriver.Quit();
			}	
		}
	}
}