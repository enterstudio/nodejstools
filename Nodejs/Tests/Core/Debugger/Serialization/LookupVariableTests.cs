// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.NodejsTools.Debugger;
using Microsoft.NodejsTools.Debugger.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace NodejsTests.Debugger.Serialization
{
    [TestClass]
    public class LookupVariableTests
    {
        [TestMethod, Priority(0), TestCategory("Debugging")]
        public void CreateLookupVariable()
        {
            // Arrange
            var parent = new NodeEvaluationResult(0, null, null, null, null, null, NodeExpressionType.None, null);
            JObject json = SerializationTestData.GetLookupJsonProperty();
            Dictionary<int, JToken> references = SerializationTestData.GetLookupJsonReferences();

            // Act
            var result = new NodeLookupVariable(parent, json, references);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(NodePropertyAttributes.None, result.Attributes);
            Assert.IsNull(result.Class);
            Assert.AreEqual(54, result.Id);
            Assert.AreEqual("first", result.Name);
            Assert.AreEqual(parent, result.Parent);
            Assert.IsNull(result.StackFrame);
            Assert.AreEqual("1", result.Text);
            Assert.AreEqual(NodePropertyType.Field, result.Type);
            Assert.AreEqual("number", result.TypeName);
            Assert.AreEqual("1", result.Value);
        }

        [TestMethod, Priority(0), TestCategory("Debugging")]
        public void CreateLookupVariableWithNullParent()
        {
            // Arrange
            JObject json = SerializationTestData.GetLookupJsonProperty();
            Dictionary<int, JToken> references = SerializationTestData.GetLookupJsonReferences();

            // Act
            var result = new NodeLookupVariable(null, json, references);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(NodePropertyAttributes.None, result.Attributes);
            Assert.IsNull(result.Class);
            Assert.AreEqual(54, result.Id);
            Assert.AreEqual("first", result.Name);
            Assert.IsNull(result.Parent);
            Assert.IsNull(result.StackFrame);
            Assert.AreEqual("1", result.Text);
            Assert.AreEqual(NodePropertyType.Field, result.Type);
            Assert.AreEqual("number", result.TypeName);
            Assert.AreEqual("1", result.Value);
        }

        [TestMethod, Priority(0), TestCategory("Debugging")]
        public void CreateLookupVariableWithNullJsonValue()
        {
            // Arrange
            var parent = new NodeEvaluationResult(0, null, null, null, null, null, NodeExpressionType.None, null);
            Exception exception = null;
            Dictionary<int, JToken> references = SerializationTestData.GetLookupJsonReferences();
            NodeLookupVariable result = null;

            // Act
            try
            {
                result = new NodeLookupVariable(parent, null, references);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.IsNull(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
        }

        [TestMethod, Priority(0), TestCategory("Debugging")]
        public void CreateLookupVariableWithNullJsonReferences()
        {
            // Arrange
            var parent = new NodeEvaluationResult(0, null, null, null, null, null, NodeExpressionType.None, null);
            JObject json = SerializationTestData.GetLookupJsonProperty();
            Exception exception = null;
            NodeLookupVariable result = null;

            // Act
            try
            {
                result = new NodeLookupVariable(parent, json, null);
            }
            catch (Exception e)
            {
                exception = e;
            }

            // Assert
            Assert.IsNull(result);
            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(ArgumentNullException));
        }
    }
}

