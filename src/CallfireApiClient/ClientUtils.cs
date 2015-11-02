package com.callfire.api.client;

import com.callfire.api.client.api.common.model.CallfireModel;
import com.callfire.api.client.api.common.model.request.ConvertToString;
import com.callfire.api.client.api.common.model.request.QueryParamIgnore;
import org.apache.commons.lang3.StringUtils;
import org.apache.http.NameValuePair;
import org.apache.http.message.BasicNameValuePair;

import java.lang.reflect.Field;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Objects;

/**
 * Utility class
 *
 * @since 1.0
 */
public final class ClientUtils {
    private ClientUtils() {
    }

    /**
     * Add query param to name-value query list if it's value not null
     *
     * @param name        parameter name
     * @param value       parameter value
     * @param queryParams parameters list
     */
    public static void addQueryParamIfSet(String name, Object value, List<NameValuePair> queryParams) {
        if (name != null && value != null && queryParams != null) {
            queryParams.add(new BasicNameValuePair(name, Objects.toString(value)));
        }
    }

    /**
     * Add {@link Iterable} value as query param to name-value query list
     *
     * @param name        parameter name
     * @param value       collection with values
     * @param queryParams parameters list
     */
    public static void addQueryParamIfSet(String name, Iterable value, List<NameValuePair> queryParams) {
        if (name != null && value != null && queryParams != null) {
            for (Object o : value) {
                queryParams.add(new BasicNameValuePair(name, o.toString()));
            }
        }
    }

    /**
     * Method traverses request object using reflection and build {@link List} of {@link NameValuePair} from it
     *
     * @param request request
     * @param <T>     type of request
     * @return list contains query parameters
     * @throws CallfireClientException in case IllegalAccessException occurred
     */
    public static <T extends CallfireModel> List<NameValuePair> buildQueryParams(T request)
        throws CallfireClientException {
        List<NameValuePair> params = new ArrayList<>();
        Class<?> superclass = request.getClass().getSuperclass();
        while (superclass != null) {
            readFields(request, params, superclass);
            superclass = superclass.getSuperclass();
        }
        readFields(request, params, request.getClass());
        return params;
    }

    private static void readFields(Object request, List<NameValuePair> params, Class<?> clazz) {
        for (Field field : clazz.getDeclaredFields()) {
            try {
                readField(request, params, field);
            } catch (IllegalAccessException e) {
                throw new CallfireClientException(e);
            }
        }
    }

    private static void readField(Object request, List<NameValuePair> params, Field field)
        throws IllegalAccessException {
        field.setAccessible(true);
        if (field.isAnnotationPresent(QueryParamIgnore.class) &&
            field.getAnnotation(QueryParamIgnore.class).enabled()) {
            return;
        }
        Object value = field.get(request);
        if (value != null) {
            if (field.isAnnotationPresent(ConvertToString.class) && value instanceof Iterable) {
                value = StringUtils.join((Iterable) value, field.getAnnotation(ConvertToString.class).separator());
                if (StringUtils.isEmpty((String) value)) {
                    return;
                }
            }
            if (value instanceof Iterable) {
                for (Object o : (Iterable) value) {
                    params.add(new BasicNameValuePair(field.getName(), o.toString()));
                }
                return;
            }
            if (value instanceof Date) {
                value = ((Date) value).getTime();
            }
            params.add(new BasicNameValuePair(field.getName(), value.toString()));
        }
    }
}
