using System;
using NUnit.Framework;
using CallfireApiClient.Api.Account.Model;
using RestSharp;

namespace CallfireApiClient.Tests.Api.Account
{
    [TestFixture()]
    public class MeApiTest : AbstractApiTest
    {
        [Test()]
        public void GetAccount()
        {
            string expectedJson = GetJsonPayload("/account/meApi/response/getAccount.json");
            MockRestResponse(expectedJson);
            UserAccount account = Client.MeApi.GetAccount();
            Assert.NotNull(account);
            Assert.That(Serializer.Serialize(account), Is.EqualTo(expectedJson));
        }

        [Test()]
        public void GetBillingPlanUsage()
        {
            String expectedJson = GetJsonPayload("/account/meApi/response/getBillingPlanUsage.json");
            MockRestResponse(expectedJson);
            BillingPlanUsage billingPlanUsage = Client.MeApi.GetBillingPlanUsage();
            Assert.That(Serializer.Serialize(billingPlanUsage), Is.EqualTo(expectedJson));
        }

        //            @Test
        //            public void testGetCallerIds() throws Exception {
        //                String expectedJson = getJsonPayload(BASE_PATH + "/account/meApi/response/getCallerIds.json");
        //                mockHttpResponse(mockHttpClient, mockHttpResponse, expectedJson);
        //
        //                List<CallerId> callerIds = client.meApi().getCallerIds();
        //                assertThat(jsonConverter.serialize(new ListHolder<>(callerIds)), equalToIgnoringWhiteSpace(expectedJson));
        //            }
        //
        //            @Test
        //            public void testSendVerificationCode() throws Exception {
        //                ArgumentCaptor<HttpUriRequest> captor = mockHttpResponse(mockHttpClient, mockHttpResponse);
        //                String callerId = "1234567890";
        //
        //                client.meApi().sendVerificationCode(callerId);
        //
        //                HttpUriRequest arg = captor.getValue();
        //                assertEquals(HttpPost.METHOD_NAME, arg.getMethod());
        //                assertThat(arg.getURI().toString(), containsString(callerId));
        //                assertNull(extractHttpEntity(arg));
        //            }
        //
        //            @Test
        //            public void testVerifyCallerId() throws Exception {
        //                String expectedJson = getJsonPayload(BASE_PATH + "/account/meApi/response/verifyCallerId.json");
        //                ArgumentCaptor<HttpUriRequest> captor = mockHttpResponse(mockHttpClient, mockHttpResponse, expectedJson);
        //
        //                CallerIdVerificationRequest request = CallerIdVerificationRequest.create()
        //                    .callerId("1234567890")
        //                    .verificationCode("0987654321")
        //                    .build();
        //                Boolean verified = client.meApi().verifyCallerId(request);
        //                assertThat(jsonConverter.serialize(verified), equalToIgnoringWhiteSpace(expectedJson));
        //
        //                HttpUriRequest arg = captor.getValue();
        //                assertEquals(HttpPost.METHOD_NAME, arg.getMethod());
        //                assertEquals(jsonConverter.serialize(request), extractHttpEntity(arg));
        //                assertEquals(2, arg.getAllHeaders().length);
        //                assertNotNull(arg.getFirstHeader(HttpHeaders.AUTHORIZATION).getValue());
        //                assertEquals(APPLICATION_JSON.getMimeType(), arg.getFirstHeader(HttpHeaders.CONTENT_TYPE).getValue());
        //                assertThat(arg.getURI().toString(), containsString(request.getCallerId()));
        //            }
        //
        //            @Test
        //            public void testCreateApiCredentials() throws Exception {
        //                String responseJson = getJsonPayload(BASE_PATH + "/account/meApi/response/createApiCredentials.json");
        //                String requestJson = getJsonPayload(BASE_PATH + "/account/meApi/request/createApiCredentials.json");
        //                ArgumentCaptor<HttpUriRequest> captor = mockHttpResponse(mockHttpClient, mockHttpResponse, responseJson);
        //
        //                ApiCredentials credentials = new ApiCredentials("api_20_account");
        //                ApiCredentials apiCredentials = client.meApi().createApiCredentials(credentials);
        //                assertThat(jsonConverter.serialize(apiCredentials), equalToIgnoringWhiteSpace(responseJson));
        //
        //                HttpUriRequest arg = captor.getValue();
        //                assertEquals(HttpPost.METHOD_NAME, arg.getMethod());
        //                assertThat(extractHttpEntity(arg), equalToIgnoringWhiteSpace(requestJson));
        //            }
        //
        //            @Test
        //            public void testFindApiCredentials() throws Exception {
        //                String expectedJson = getJsonPayload(BASE_PATH + "/account/meApi/response/findApiCredentials.json");
        //                ArgumentCaptor<HttpUriRequest> captor = mockHttpResponse(mockHttpClient, mockHttpResponse, expectedJson);
        //
        //                CommonFindRequest request = CommonFindRequest.create()
        //                    .limit(1L)
        //                    .offset(5L)
        //                    .build();
        //                Page<ApiCredentials> apiCredentials = client.meApi().findApiCredentials(request);
        //                assertThat(jsonConverter.serialize(apiCredentials), equalToIgnoringWhiteSpace(expectedJson));
        //
        //                HttpUriRequest arg = captor.getValue();
        //                assertEquals(HttpGet.METHOD_NAME, arg.getMethod());
        //                assertNull(extractHttpEntity(arg));
        //                assertThat(arg.getURI().toString(), containsString("limit=1"));
        //                assertThat(arg.getURI().toString(), containsString("offset=5"));
        //            }
        //
        //            @Test
        //            public void testGetApiCredentials() throws Exception {
        //                String expectedJson = getJsonPayload(BASE_PATH + "/account/meApi/response/getApiCredentials.json");
        //                ArgumentCaptor<HttpUriRequest> captor = mockHttpResponse(mockHttpClient, mockHttpResponse, expectedJson);
        //
        //                ApiCredentials apiCredentials = client.meApi().getApiCredentials(11L, FIELDS);
        //                assertThat(jsonConverter.serialize(apiCredentials), equalToIgnoringWhiteSpace(expectedJson));
        //
        //                HttpUriRequest arg = captor.getValue();
        //                assertEquals(HttpGet.METHOD_NAME, arg.getMethod());
        //                assertNull(extractHttpEntity(arg));
        //                assertThat(arg.getURI().toString(), containsString(ENCODED_FIELDS));
        //
        //                client.meApi().getApiCredentials(11L);
        //                assertEquals(2, captor.getAllValues().size());
        //                assertThat(captor.getAllValues().get(1).getURI().toString(), not(containsString("fields")));
        //            }
        //
        //            @Test
        //            public void testDeleteApiCredentials() throws Exception {
        //                ArgumentCaptor<HttpUriRequest> captor = mockHttpResponse(mockHttpClient, mockHttpResponse);
        //
        //                client.meApi().deleteApiCredentials(11L);
        //
        //                HttpUriRequest arg = captor.getValue();
        //                assertEquals(HttpDelete.METHOD_NAME, arg.getMethod());
        //                assertNull(extractHttpEntity(arg));
        //                assertThat(arg.getURI().toString(), containsString("/11"));
        //            }
    }




}

