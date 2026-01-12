/*
 * Copyright 2025 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      https://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections.Generic;
using System.Text.Json;

using Google.GenAI.Types;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Google.GenAI.Tests {
  [TestClass]
  public class FileSearchTest {
    [TestMethod]
    public void TestFileSearchSerialization() {
      var fileSearch = new FileSearch {
        FileSearchStoreNames = new List<string> { "fileSearchStores/test-store-id" }
      };

      string json = JsonSerializer.Serialize(fileSearch);
      Assert.IsTrue(json.Contains("fileSearchStoreNames"));
      Assert.IsTrue(json.Contains("fileSearchStores/test-store-id"));
    }

    [TestMethod]
    public void TestFileSearchDeserialization() {
      string json = "{\"fileSearchStoreNames\":[\"fileSearchStores/test-store-id\"]}";
      var fileSearch = FileSearch.FromJson(json);

      Assert.IsNotNull(fileSearch);
      Assert.IsNotNull(fileSearch.FileSearchStoreNames);
      Assert.AreEqual(1, fileSearch.FileSearchStoreNames.Count);
      Assert.AreEqual("fileSearchStores/test-store-id", fileSearch.FileSearchStoreNames[0]);
    }

    [TestMethod]
    public void TestToolWithFileSearch() {
      var tool = new Tool {
        FileSearch = new FileSearch {
          FileSearchStoreNames = new List<string> { "fileSearchStores/test-store-id" }
        }
      };

      string json = JsonSerializer.Serialize(tool);
      Assert.IsTrue(json.Contains("fileSearch"));
      Assert.IsTrue(json.Contains("fileSearchStoreNames"));
    }

    [TestMethod]
    public void TestToolWithFileSearchDeserialization() {
      string json =
          "{\"fileSearch\":{\"fileSearchStoreNames\":[\"fileSearchStores/test-store-id\"]}}";
      var tool = Tool.FromJson(json);

      Assert.IsNotNull(tool);
      Assert.IsNotNull(tool.FileSearch);
      Assert.IsNotNull(tool.FileSearch.FileSearchStoreNames);
      Assert.AreEqual(1, tool.FileSearch.FileSearchStoreNames.Count);
      Assert.AreEqual("fileSearchStores/test-store-id", tool.FileSearch.FileSearchStoreNames[0]);
    }
  }
}
