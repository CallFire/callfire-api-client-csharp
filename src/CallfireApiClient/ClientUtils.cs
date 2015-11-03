using CallfireApiClient.Api.Common.Model;
using System.Collections.Specialized;
using System.Text;

namespace CallfireApiClient
{

    /// <summary>
    /// Utility class
    /// </summary>
    public class ClientUtils
    {
        private ClientUtils()
        {
        }

        /// <summary>
        /// Converts NameValueCollection to string
        /// </summary>
        /// <returns>string representation of collection</returns>
        /// <param name="queryParams">collection to convert</param>
        public static string ParamsToString(NameValueCollection queryParams)
        {
            var result = new StringBuilder();
            foreach (string key in queryParams.AllKeys)
            {
                result.AppendFormat("{0} = {1} ", key, string.Join(",", queryParams.GetValues(key)));
            }
            return result.ToString();
        }

        /**
     * Add query param to name-value query list if it's value not null
     *
     * @param name        parameter name
     * @param value       parameter value
     * @param queryParams parameters list
     */
        //    public static void addQueryParamIfSet(String name, Object value, List<NameValuePair> queryParams) {
        //        if (name != null && value != null && queryParams != null) {
        //            queryParams.add(new BasicNameValuePair(name, Objects.toString(value)));
        //        }
        //    }

        /**
     * Add {@link Iterable} value as query param to name-value query list
     *
     * @param name        parameter name
     * @param value       collection with values
     * @param queryParams parameters list
     */
        //    public static void addQueryParamIfSet(String name, Iterable value, List<NameValuePair> queryParams) {
        //        if (name != null && value != null && queryParams != null) {
        //            for (Object o : value) {
        //                queryParams.add(new BasicNameValuePair(name, o.toString()));
        //            }
        //        }
        //    }

        /**
     * Method traverses request object using reflection and build {@link List} of {@link NameValuePair} from it
     *
     * @param request request
     * @param <T>     type of request
     * @return list contains query parameters
     * @throws CallfireClientException in case IllegalAccessException occurred
     */
        public static NameValueCollection BuildQueryParams<T>(T request)
            where T : CallfireModel
        {
            NameValueCollection parameters = new NameValueCollection();
//        Class<?> superclass = request.getClass().getSuperclass();
//        while (superclass != null) {
//            readFields(request, params, superclass);
//            superclass = superclass.getSuperclass();
//        }
//        readFields(request, params, request.getClass());
            return parameters;
        }
        //
        //    private static void readFields(Object request, List<NameValuePair> params, Class<?> clazz) {
        //        for (Field field : clazz.getDeclaredFields()) {
        //            try {
        //                readField(request, params, field);
        //            } catch (IllegalAccessException e) {
        //                throw new CallfireClientException(e);
        //            }
        //        }
        //    }
        //
        //    private static void readField(Object request, List<NameValuePair> params, Field field)
        //        throws IllegalAccessException {
        //        field.setAccessible(true);
        //        if (field.isAnnotationPresent(QueryParamIgnore.class) &&
        //            field.getAnnotation(QueryParamIgnore.class).enabled()) {
        //            return;
        //        }
        //        Object value = field.get(request);
        //        if (value != null) {
        //            if (field.isAnnotationPresent(ConvertToString.class) && value instanceof Iterable) {
        //                value = StringUtils.join((Iterable) value, field.getAnnotation(ConvertToString.class).separator());
        //                if (StringUtils.isEmpty((String) value)) {
        //                    return;
        //                }
        //            }
        //            if (value instanceof Iterable) {
        //                for (Object o : (Iterable) value) {
        //                    params.add(new BasicNameValuePair(field.getName(), o.toString()));
        //                }
        //                return;
        //            }
        //            if (value instanceof Date) {
        //                value = ((Date) value).getTime();
        //            }
        //            params.add(new BasicNameValuePair(field.getName(), value.toString()));
        //        }
        //    }
    }
}