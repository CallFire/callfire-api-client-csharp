package com.callfire.api.client;

import com.callfire.api.client.api.common.model.ErrorMessage;
import com.callfire.api.client.api.common.model.ListHolder;
import com.callfire.api.client.api.common.model.ResourceId;
import com.fasterxml.jackson.core.type.TypeReference;

import java.io.InputStream;
import java.util.List;
import java.util.Map;

/**
 * Client constants
 *
 * @since 1.0
 */
public interface ClientConstants {
    String BASE_PATH_PROPERTY = "com.callfire.api.client.path";
    String USER_AGENT_PROPERTY = "com.callfire.api.client.version";
    String CLIENT_CONFIG_FILE = "/com/callfire/api/client/callfire.properties";

    String PLACEHOLDER = "\\{\\}";
    // Use ISO 8601 format for date and datetime.
    // See https://en.wikipedia.org/wiki/ISO_8601
    String DATE_FORMAT_PATTERN = "yyyy-MM-dd'T'HH:mm:ss.SSSZ";
    String GENERIC_HELP_LINK = "https://answers.callfire.com/hc/en-us";

    /**
     * Jackson TypeReference types for valid serialization/deserialization
     */
    interface Type {
        TypeReference<String> STRING_TYPE = new TypeReference<String>() {
        };
        TypeReference<Void> VOID_TYPE = new TypeReference<Void>() {
        };
        TypeReference<Boolean> BOOLEAN_TYPE = new TypeReference<Boolean>() {
        };
        TypeReference<InputStream> INPUT_STREAM_TYPE = new TypeReference<InputStream>() {
        };
        TypeReference<List<String>> LIST_OF_STRINGS_TYPE = new TypeReference<List<String>>() {
        };
        TypeReference<Map<String, String>> MAP_OF_STRINGS_TYPE = new TypeReference<Map<String, String>>() {
        };
        TypeReference<ResourceId> RESOURCE_ID_TYPE = new TypeReference<ResourceId>() {
        };
        TypeReference<ListHolder<ResourceId>> LIST_OF_RESOURCE_ID_TYPE = new TypeReference<ListHolder<ResourceId>>() {
        };
        TypeReference<ErrorMessage> ERROR_MESSAGE_TYPE = new TypeReference<ErrorMessage>() {
        };
    }
}
